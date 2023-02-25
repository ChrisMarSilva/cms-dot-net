using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly string _token;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            _logger.LogInformation("Web.ProductController.ProductIndex()");

            var products = await _productService.FindAllProducts("");
            return View(products);
        }

        [Authorize]
        public async Task<IActionResult> ProductCreate()
        {
            _logger.LogInformation("Web.ProductController.ProductCreate()");

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            _logger.LogInformation("Web.ProductController.ProductCreate(model)");

            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProduct(model, token);
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            _logger.LogInformation("Web.ProductController.ProductUpdate(id)");

            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindProductById(id, token);
            if (model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductViewModel model)
        {
            _logger.LogInformation("Web.ProductController.ProductUpdate(model)");

            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProduct(model, token);
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            _logger.LogInformation("Web.ProductController.ProductDelete(id)");

            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindProductById(id, token);
            if (model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel model)
        {
            _logger.LogInformation("Web.ProductController.ProductDelete(model)");

            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(model.Id, token);
            if (response)
                return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
    }
}
