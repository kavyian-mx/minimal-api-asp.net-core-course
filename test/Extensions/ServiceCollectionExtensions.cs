// Extensions/ServiceCollectionExtensions.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Entites;
using Service;

namespace Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                                                            IConfiguration config)
    {
        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Course API", Version = "v1" });
        });

        // Http logging
        services.AddHttpLogging(c =>
            c.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All);

        // EF Core
        services.AddDbContext<DataBase.Course>(opt =>
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        // DI سرویس‌ها
        services.AddScoped<CategoryService>();
        services.AddScoped<TeacherService>();
        services.AddScoped<ProductService>();
        return services;
    }
}
