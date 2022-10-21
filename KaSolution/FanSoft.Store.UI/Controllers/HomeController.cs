using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FanSoft.Store.UI.Infra;

namespace FanSoft.Store.UI.Controllers
{
    //[Route("HomeApp")]
    public class HomeController : Controller
    {

        // private ILogger<HomeController> _logger;
        // private SeriLogFile _logger;

        // public HomeController(ILogger<HomeController> logger)
        // public HomeController(SeriLogFile logger)
        public HomeController()
        {
            //_logger = logger;
        }

        //public IActionResult Index() => View();

        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        //[HttpGet("IndexApp")]
        //public string Index(string msg) => "Teste sem Texto";

        //[HttpGet("IndexApp/{msg}")]
        //public string Index(string msg) => "Teste com Texto - " + msg;

        public IActionResult About()
        {
            return View();
        }

        public string TesteException()
        {
            var num1 = 2;
            var num2 = 0;
            var result = 0;

            try
            {
                result = num1 / num2;
            }
            catch (Exception)
            {
                return "Erro seu trouxa!!!";
            }

            return "Resultado = " + result.ToString();
        }

        public string TesteException2()
        {
            var num1 = 2;
            var num2 = 0;
            var result = num1 / num2;
            return "Resultado = " + result.ToString();
        }

        [HttpGet("error")]
        public IActionResult Error([FromServices]ILogCustom _logger)
        {

            var statusCode = HttpContext.Response.StatusCode;
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            var path = "";
            var exceptionErro = "";

            var msg = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (msg != null)
            {
                path = msg.Path;
                exceptionErro = msg.Error.Message;
            }

            _logger.LogInformation("======================================================================");
            _logger.LogInformation("ERROS CMS");
            _logger.LogInformation("======================================================================");
            _logger.LogInformation("StatusCode: " + statusCode);
            _logger.LogInformation("UserAgent: " + userAgent);
            _logger.LogInformation("Path: " + path);
            _logger.LogInformation("ExceptionErro: " + exceptionErro);
            _logger.LogInformation("======================================================================");

            ViewBag.Title = "Erro Padrão";
            ViewBag.StatusCode = statusCode;
            ViewBag.UserAgent = userAgent;
            ViewBag.Path = path;
            ViewBag.ExceptionErro = exceptionErro;

            return View();
        }

    }
}
