// Extensions/ServiceCollectionExtensions.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Entites;
using Service;
using System.Reflection;

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
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Course API",
                Version = "v1",
                Description = "API برای مدیریت دوره‌ها، معلم‌ها و دسته‌بندی‌ها"
            });

            // پشتیبانی از Tagهای گروهی (WithTags)
            c.TagActionsBy(api =>
            {
                var groupName = api.GroupName;
                if (!string.IsNullOrWhiteSpace(groupName))
                    return new[] { groupName };

                if (api.ActionDescriptor.EndpointMetadata.OfType<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupNameProvider>().FirstOrDefault() is { } provider)
                    return new[] { provider.GroupName };

                return new[] { api.ActionDescriptor.DisplayName ?? "بدون گروه" };
            });

            c.DocInclusionPredicate((name, api) => true);

            // برای نمایش توضیح پارامترها از XML doc استفاده می‌کنیم
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
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
        services.AddScoped<CommentService>();

        return services;
    }
}

