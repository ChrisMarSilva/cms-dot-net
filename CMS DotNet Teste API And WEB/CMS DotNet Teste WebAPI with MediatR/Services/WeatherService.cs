namespace CMS_DotNet_Teste_WebAPI_with_MediatR.Services;

public class WeatherService
{
    private static readonly string[] Summaries = { 
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" 
    };

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForCityAsync(string city, int days)
    {
        await Task.Delay(50);
        var results = Enumerable
                .Range(1, days)
                .Select(index => new WeatherForecast{
                    Date = DateTime.Now.AddDays(index), 
                    TemperatureC = Random.Shared.Next(-20, 55), 
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        return results;
    }
}

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; } = string.Empty;
}
