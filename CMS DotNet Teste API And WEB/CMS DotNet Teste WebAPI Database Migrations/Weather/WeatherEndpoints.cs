using CMS_DotNet_Teste_WebAPI_Database_Migrations.Movies;

namespace CMS_DotNet_Teste_WebAPI_Database_Migrations.Weather;

public static class WeatherEndpoints
{
    public static void MapWeatherEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("weatherforecast");

        group.MapGet("/", () =>
        {
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

            var forecast = Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                .ToArray();

            return forecast;
        })
        .WithName("GetWeatherForecast");
    }

  
}