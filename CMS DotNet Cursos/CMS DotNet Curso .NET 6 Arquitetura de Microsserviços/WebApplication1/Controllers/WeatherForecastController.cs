using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain;
using WebApplication1.Services.WeatherForecastService;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController( 
            ILogger<WeatherForecastController> logger,
            IWeatherForecastService weatherForecastService)
        {
            this._logger = logger;
            this._weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            this._logger.LogInformation("GetWeatherForecast");
            return this._weatherForecastService.Get();
        }
    }
}