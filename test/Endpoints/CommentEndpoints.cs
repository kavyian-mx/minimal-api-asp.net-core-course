using Entites;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Endpoints;

public static class CommentEndpoints
{
    public static IEndpointRouteBuilder MapCommentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/comments").WithTags("Comments");

        // 📌 دریافت همه کامنت‌ها
        group.MapGet("/", async (CommentService service) =>
            Results.Ok(await service.GetListComment()));

        // 📌 دریافت یک کامنت بر اساس ID
        group.MapGet("/{id:int}", async (int id, CommentService service) =>
        {
            var comment = await service.GetCommentAsyncId(id);
            return comment is not null ? Results.Ok(comment) : Results.NotFound();
        });

        // 📌 دریافت همه کامنت‌های مربوط به یک محصول
        group.MapGet("/product/{productId:int}", async (int productId, CommentService service) =>
        {
            var comments = await service.GetCommentsByProductIdAsync(productId);
            return Results.Ok(comments);
        });

        // 📌 افزودن کامنت جدید
        group.MapPost("/", async ([FromBody] Comment comment, CommentService service) =>
        {
            var id = await service.InsertAsync(comment);
            return Results.Created($"/comments/{id}", comment);
        });

        // ✏️ ویرایش فقط متن کامنت
        group.MapPut("/{id:int}", async (int id, [FromBody] Comment commentUpdate, CommentService service) =>
        {
            if (id != commentUpdate.Id)
                return Results.BadRequest("شناسه‌ی ارسال‌شده با شناسه‌ی بدنه یکی نیست.");

            var updated = await service.CommentUpdate(commentUpdate);
            return updated ? Results.Ok("کامنت با موفقیت ویرایش شد.") : Results.NotFound();
        });

        // ❌ حذف کامنت
        group.MapDelete("/{id:int}", async (int id, CommentService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.Ok("کامنت حذف شد.") : Results.NotFound();
        });

        return app;
    }
}
