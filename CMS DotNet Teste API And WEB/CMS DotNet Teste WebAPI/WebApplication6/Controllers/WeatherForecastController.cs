using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        public ITeste1 _t1 { get; }
        public ITeste2 _t2 { get; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITeste1 t1, ITeste2 t2)
        {
            _logger = logger;
            _t1 = t1;
            _t2 = t2;
        }

        public IActionResult Get()
        {
            return Ok( new { t1 = _t1.GetNumero() , t2 = _t2.GetNumero() }  );
        }

    }
}
