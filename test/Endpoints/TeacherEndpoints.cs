using Entites;
using Service;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints;

public static class TeacherEndpoints
{
    public static void MapTeacherEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/teachers").WithTags("Teachers");

        // دریافت همه معلم‌ها
        group.MapGet("/", async (TeacherService service) =>
        {
            var teachers = await service.GetTeachers();
            return Results.Ok(teachers);
        }).WithName("GetAllTeachers");

        // دریافت معلم بر اساس آیدی
        group.MapGet("/{id:int}", async (int id, TeacherService service) =>
        {
            var teacher = await service.GetTeacherIdAsync(id);
            return teacher is null ? Results.NotFound() : Results.Ok(teacher);
        }).WithName("GetTeacherById");

        // جستجو با نام یا نام خانوادگی
        group.MapGet("/search", async ([FromQuery] string? firstName, [FromQuery] string? lastName, TeacherService service) =>
        {
            var result = await service.SearchAsync(lastName, firstName);
            return Results.Ok(result);
        }).WithName("SearchTeachers");

        // افزودن معلم جدید
        group.MapPost("/", async ([FromBody] Teacher teacher, TeacherService service) =>
        {
            var id = await service.InsertAsync(teacher);
            return Results.Created($"/teachers/{id}", teacher);
        }).WithName("CreateTeacher");

        // ویرایش معلم
        group.MapPut("/{id:int}", async (int id, [FromBody] Teacher updated, TeacherService service) =>
        {
            var exists = await service.Exists(id);
            if (!exists) return Results.NotFound();

            updated.Id = id;
            var updatedOk = await service.UpdateAsync(updated);
            return updatedOk ? Results.NoContent() : Results.BadRequest();
        }).WithName("UpdateTeacher");

        // حذف معلم
        group.MapDelete("/{id:int}", async (int id, TeacherService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        }).WithName("DeleteTeacher");

        // بارگذاری فایل عکس معلم
        group.MapPost("/upload", async (HttpRequest request, TeacherService service) =>
        {
            var form = await request.ReadFormAsync();

            var file = form.Files.GetFile("file");
            var firstName = form["firstName"];
            var lastName = form["lastName"];
            var birthdayStr = form["birthday"];

            if (file is null || file.Length == 0)
                return Results.BadRequest("فایل ارسال نشده یا خالی است.");

            if (!DateTime.TryParse(birthdayStr, out var birthday))
                return Results.BadRequest("فرمت تاریخ تولد نامعتبر است.");

            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot", "uploads", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            var teacher = new Teacher
            {
                FirstName = firstName!,
                LastName = lastName!,
                Birtday = birthday,
                UrlImage = $"/uploads/{fileName}"
            };

            await service.InsertAsync(teacher);
            return Results.Created($"/teachers/{teacher.Id}", teacher);
        }).DisableAntiforgery(); // برای فرم آپلود در صورت نیاز به غیرفعال‌سازی CSRF
    }
}
