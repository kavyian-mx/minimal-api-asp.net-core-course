// Endpoints/CategoryEndpoints.cs
using Entites;
using Microsoft.AspNetCore.Builder;
using Service;

namespace Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/sentallcategories", async (CategoryService svc) =>
        {
            var list = await svc.GetCategories();
            return Results.Ok(list);
        });

        app.MapGet("/categories/{id:int}", async (int id, CategoryService svc) =>
        {
            var cat = await svc.GetByIdAsync(id);
            return cat is null ? Results.NotFound() : Results.Ok(cat);
        });

        app.MapPost("/categories", async (Category cat, CategoryService svc) =>
        {
            var newId = await svc.InsertAsync(cat);
            return Results.Created($"/categories/{newId}", cat);
        });

        app.MapPut("/categories/{id:int}", async (int id, Category dto, CategoryService svc) =>
        {
            var exists = await svc.GetByIdAsync(id);
            if (exists is null) return Results.NotFound();

            exists.Name = dto.Name;
            await svc.UpdateAsync(exists);
            return Results.NoContent();
        });

        app.MapDelete("/categories/{id:int}", async (int id, CategoryService svc) =>
        {
            var deleted = await svc.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}
