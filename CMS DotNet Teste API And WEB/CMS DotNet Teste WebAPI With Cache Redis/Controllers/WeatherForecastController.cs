using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("ping")]
        public string Ping() => "Ping";
        // public IActionResult Ping() => Ok();

        //[HttpGet(Name = "health")]
        //public IActionResult Health() => Ok();
        //public string Health() => "Health";

        //[HttpGet(Name = "data")]
        //public IActionResult Data() => Ok();
        //public string GetData() => "Data";

        //[HttpGet(Name = "getall")]
        //public IActionResult Getall() => Ok();
        //public string Getall() => "Getall";

    }
}