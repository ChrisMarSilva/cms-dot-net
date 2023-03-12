using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.App.TempoDeVida.Models;
using Web.App.TempoDeVida.Services.Interfaces;

namespace Web.App.TempoDeVida.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IOperationTransient _transientOperation1;
        public readonly IOperationTransient _transientOperation2;
        public readonly IOperationScoped _scopedOperation1;
        public readonly IOperationScoped _scopedOperation2;
        public readonly IOperationSingleton _sinletonOperation1;
        public readonly IOperationSingleton _sinletonOperation2;

        public HomeController(
            ILogger<HomeController> logger,
            IOperationTransient transientOperation1,
            IOperationTransient transientOperation2,
            IOperationScoped scopedOperation1,
            IOperationScoped scopedOperation2,
            IOperationSingleton sinletonOperation1,
            IOperationSingleton sinletonOperation2
            )
        {
            _logger = logger;
            _transientOperation1 = transientOperation1;
            _transientOperation2 = transientOperation2;
            _scopedOperation1 = scopedOperation1;
            _scopedOperation2 = scopedOperation2;
            _sinletonOperation1 = sinletonOperation1;
            _sinletonOperation2 = sinletonOperation2;
        }

        public IActionResult Index()
        {
            //return
            //    $"Transient1    : {_transientOperation1.OperationId} \n" +
            //    $"Transient2    : {_transientOperation2.OperationId} \n\n" +
            //    $"Scoped1       : {_scopedOperation1.OperationId} \n" +
            //    $"Scoped2       : {_scopedOperation2.OperationId} \n\n" +
            //    $"Singleton1    : {_sinletonOperation1.OperationId} \n" +
            //    $"Singleton2    : {_sinletonOperation2.OperationId}";

            ViewBag.Transient1 = _transientOperation1.OperationId;
            ViewBag.Transient2 = _transientOperation2.OperationId;
            ViewBag.Transient3 = _transientOperation1.OperationId == _transientOperation2.OperationId;

            ViewBag.Scoped1 = _scopedOperation1.OperationId;
            ViewBag.Scoped2 = _scopedOperation2.OperationId;
            ViewBag.Scoped3 = _scopedOperation1.OperationId == _scopedOperation2.OperationId;

            ViewBag.Singleton1 = _sinletonOperation1.OperationId;
            ViewBag.Singleton2 = _sinletonOperation2.OperationId;
            ViewBag.Singleton3 = _sinletonOperation1.OperationId == _sinletonOperation2.OperationId;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}