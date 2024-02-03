using Microsoft.AspNetCore.Mvc;
using Test1.ApiService.Features.WeatherForecastFeature;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddTransient<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/weatherforecast", async ([FromServices] WeatherForecastService service) =>
{
    var result = await service.GetForecast();
    return Results.Ok(result);
});

app.MapDefaultEndpoints();

app.Run();


