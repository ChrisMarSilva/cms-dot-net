﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService,
            ICartService cartService
            )
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
            _logger.LogInformation("Web.HomeController");
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Web.HomeController.Index()");

            var products = await _productService.FindAllProducts("");
            return View(products);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Web.HomeController.Details()");

            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindProductById(id, token);

            return View(model);
        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductViewModel model)
        {
            _logger.LogInformation("Web.HomeController.DetailsPost()");

            var token = await HttpContext.GetTokenAsync("access_token");

            //CartViewModel cart = new()
            //{
            //    CartHeader = new CartHeaderViewModel
            //    {
            //        UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
            //    }
            //};
            //
            //CartDetailViewModel cartDetail = new CartDetailViewModel()
            //{
            //    Count = model.Count,
            //    ProductId = model.Id,
            //    Product = await _productService.FindProductById(model.Id, token)
            //};

            var cart = new CartViewModel();
            cart.CartHeader = new CartHeaderViewModel();
            cart.CartHeader.UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var cartDetail = new CartDetailViewModel();
            cartDetail.Count = model.Count;
            cartDetail.ProductId = model.Id;
            cartDetail.Product = await _productService.FindProductById(model.Id, token);

            var cartDetails = new List<CartDetailViewModel>();
            cartDetails.Add(cartDetail);
            cart.CartDetails = cartDetails;

            var response = await _cartService.AddItemToCart(cart, token);

            if (response != null)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Web.HomeController.Privacy()");

            return View();
        }
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Web.HomeController.Error()");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            _logger.LogInformation("Web.HomeController.Login()");

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            _logger.LogInformation("Web.HomeController.Logout()");

            return SignOut("Cookies", "oidc");
        }
    }
}