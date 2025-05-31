using Entites;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(c =>
    c.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All
);

builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

builder.Services.AddScoped<CategoryService>();

builder.Services.AddDbContext<DataBase.Course>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/sentallcategories", async (CategoryService categoryService) =>
{
    var categories = await categoryService.GetCategories();
    return Results.Ok(categories);
});

app.MapPost("/categories", async (Category category, CategoryService service) =>
{
    var newId = await service.InsertAsync(category);
    return Results.Created($"/categories/{newId}", category);
});


app.MapGet("/categories/{id:int}", async (int id, CategoryService service) =>
{
    var category = await service.GetByIdAsync(id);
    return category is null ? Results.NotFound() : Results.Ok(category);
});

app.MapPut("/categories/{id}", async (int id, Category inputCategory, CategoryService service) =>
{
    var existing = await service.GetByIdAsync(id);
    if (existing == null)
        return Results.NotFound();

    existing.Name = inputCategory.Name;
    await service.UpdateAsync(existing);
    return Results.NoContent();
});

app.MapDelete("/categories/{id:int}", async (int id, CategoryService service) =>
{
    var deleted = await service.DeleteAsync(id);

    return deleted ? Results.NoContent() : Results.NotFound();
});


app.Run();

