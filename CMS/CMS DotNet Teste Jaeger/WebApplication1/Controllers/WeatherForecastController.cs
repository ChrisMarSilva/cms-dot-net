using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Propagation;
using OpenTracing.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Domain;

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
        private readonly ITracer _tracer;
        private object request;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITracer tracer)
        {
            _logger = logger;
            _tracer = tracer;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            using var scope0 = _tracer.BuildSpan("Get").StartActive(true);
            // var span = scope0.Span

            // using (_tracer.BuildSpan("waitingForValues").StartActive(finishSpanOnDispose: true))
            //  scopeA.Span.SetTag("customer.document", "12313213");
            // scopeA.Span.Finish();

            var span = this._tracer.ScopeManager.Active.Span
               .SetTag(Tags.SpanKind, Tags.SpanKindClient)
               .SetTag(Tags.HttpMethod, "GET")
               .SetTag(Tags.Error, true);


            System.Threading.Thread.Sleep(1000 * 1);
            span.Log("00001");

            using var scope = this._tracer.BuildSpan("#001").StartActive(true);

            System.Threading.Thread.Sleep(1000 * 2);
            span.Log("00001");
            span.Log("00002");

            using var scopeA = this._tracer.BuildSpan("#002").StartActive(true);

            System.Threading.Thread.Sleep(1000 * 3);
            span.Log("00001");
            span.Log("00002");
            span.Log("00003");

            using var scopeB = this._tracer.BuildSpan("#003").StartActive(true);

            System.Threading.Thread.Sleep(1000 * 4);
            span.Log("00001");
            span.Log("00002");
            span.Log("00003");
            span.Log("00004");

            var dictionary = new Dictionary<string, string>();
            this._tracer.Inject(span.Context, BuiltinFormats.HttpHeaders, new TextMapInjectAdapter(dictionary));

            var rng = new Random();

            return Enumerable.Range(1, 100).Select(index => new WeatherForecast
            {
                Index = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
