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

app.MapGet("/", () => "Hello World!");

app.MapGet("/Catagory", (CategoryService categoryservice) =>
{
    return Results.Ok<List<Category>>(categoryservice.GetCategories());
});

app.MapPost("/categories", async (Category category, CategoryService service) =>
{
    var newId = await service.InsertAsync(category);
    return Results.Created($"/categories/{newId}", category);
});

app.Run();

