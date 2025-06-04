using Extensions;          // ?  for AddApplicationServices
using Endpoints;           // ?  for MapCategoryEndpoints
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

//ù ”—Ê?”ùÂ«
builder.Services.AddApplicationServices(builder.Configuration);

// ›?· — ·«ê
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

var app = builder.Build();

// MiddlewareÂ«
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpLogging();
app.UseStaticFiles();

// EndpointÂ«
app.MapCategoryEndpoints();
app.MapTeacherEndpoints();
app.MapProductEndpoints();
app.MapCommentEndpoints();

app.Run();
