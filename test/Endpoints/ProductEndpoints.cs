using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service;
using Entites;

namespace Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products").WithTags("Products");

        group.MapGet("/", async (ProductService service) =>
            Results.Ok(await service.GetProducts()));

        group.MapGet("/{id:int}", async (int id, ProductService service) =>
        {
            var product = await service.GetProductAsyncId(id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        });

        group.MapGet("/search", async ([FromQuery] string? title, ProductService service) =>
            Results.Ok(await service.SearchAsync(title)));

        group.MapPost("/", async ([FromBody] Product product, ProductService service) =>
        {
            var id = await service.InsertAsync(product);
            return Results.Created($"/products/{id}", product);
        });

        group.MapPut("/{id:int}", async (int id, [FromBody] Product updatedProduct, ProductService service) =>
        {
            if (id != updatedProduct.Id)
                return Results.BadRequest("شناسه با آدرس هم‌خوانی ندارد.");

            var result = await service.UpdateAsync(updatedProduct);
            return result ? Results.Ok() : Results.NotFound();
        });

        group.MapDelete("/{id:int}", async (int id, ProductService service) =>
        {
            var result = await service.DeleteProduct(id);
            return result ? Results.Ok() : Results.NotFound();
        });

        return app;
    }
}
