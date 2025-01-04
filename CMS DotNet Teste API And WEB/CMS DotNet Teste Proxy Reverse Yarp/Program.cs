//using Microsoft.AspNetCore.RateLimiting;
//using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("Fixed", config =>
//    {
//        config.PermitLimit = 5;
//        config.Window = TimeSpan.FromMinutes(1);
//        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        config.QueueLimit = 2;
//    });

//    options.OnRejected = async (context, cancellationToken) =>
//    {
//        context.HttpContext.Response.StatusCode = 429;
//        await context.HttpContext.Response.WriteAsJsonAsync(new { Message = "Você excedeu o limite de requisições. Tente novamente mais tarde." }, cancellationToken: cancellationToken);
//    };
//});

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();
//app.UseRateLimiter();
app.UseHttpsRedirection();
app.MapReverseProxy();

app.MapGet("/weatherforecast", () =>
{
    var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi()
.RequireRateLimiting("Fixed");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
