namespace Webhook.Api.WeatherForecast;

internal static class WeatherForecastEndpoint
{
    public static void AddMapWeatherEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("weatherforecast");
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        group.MapGet("/", () =>
        {   
            var forecast = Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                (
                    Date: DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC: Random.Shared.Next(-20, 55),
                    Summary: summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");
    }
}