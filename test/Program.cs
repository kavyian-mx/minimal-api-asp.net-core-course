using Extensions;          // ?  for AddApplicationServices
using Endpoints;           // ?  for MapCategoryEndpoints
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

//� ���?ӝ��
builder.Services.AddApplicationServices(builder.Configuration);

// �?��� �ǐ
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

var app = builder.Build();

// Middleware��
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpLogging();
app.UseStaticFiles();

// Endpoint��
app.MapCategoryEndpoints();
app.MapTeacherEndpoints();
app.MapProductEndpoints();
app.MapCommentEndpoints();

app.Run();
