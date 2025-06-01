using Entites;
using Service;

namespace Endpoints;

public static class TeacherEndpoints
{
    public static void MapTeacherEndpoints(this WebApplication app)
    {
        // دریافت همه‌ی معلم‌ها
        app.MapGet("/teachers", async (TeacherService service) =>
        {
            var teachers = await service.GetTeachers();
            return Results.Ok(teachers);
        });

        // دریافت معلم خاص بر اساس آیدی
        app.MapGet("/teachers/{id:int}", async (int id, TeacherService service) =>
        {
            var teacher = await service.GetTeacherIdAsync(id);
            return teacher is null ? Results.NotFound() : Results.Ok(teacher);
        });

        // جستجو
        app.MapGet("/teachers/search", async (string? firstName, string? lastName, TeacherService service) =>
        {
            var result = await service.SearchAsync(lastName, firstName);
            return Results.Ok(result);
        });

        // افزودن معلم جدید
        app.MapPost("/teachers", async (Teacher teacher, TeacherService service) =>
        {
            var id = await service.InsertAsync(teacher);
            return Results.Created($"/teachers/{id}", teacher);
        });

        // ویرایش معلم
        app.MapPut("/teachers/{id:int}", async (int id, Teacher updated, TeacherService service) =>
        {
            var exists = await service.Exists(id);
            if (!exists) return Results.NotFound();

            updated.Id = id;
            var updatedOk = await service.UpdateAsync(updated);
            return updatedOk ? Results.NoContent() : Results.BadRequest();
        });

        // حذف معلم
        app.MapDelete("/teachers/{id:int}", async (int id, TeacherService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}
