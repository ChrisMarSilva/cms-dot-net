﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(
            ILogger<CartController> logger, 
            IProductService productService,
            ICartService cartService,
            ICouponService couponService
            )
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
            _logger.LogInformation("Web.CartController");
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            _logger.LogInformation("Web.CartController.CartIndex()");

            var response = await this.FindUserCart();
            return View(response);
        }

        public async Task<IActionResult> Remove(int id)
        {
            _logger.LogInformation("Web.CartController.Remove()");

            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveFromCart(id, token);

            if (response)
                return RedirectToAction(nameof(CartIndex));

            return View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel model)
        {
            _logger.LogInformation("Web.CartController.ApplyCoupon()");

            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.ApplyCoupon(model, token);

            if (response)
                return RedirectToAction(nameof(CartIndex));

            return View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {
            _logger.LogInformation("Web.CartController.RemoveCoupon()");

            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveCoupon(userId, token);

            if (response)
                return RedirectToAction(nameof(CartIndex));

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            _logger.LogInformation("Web.CartController.Checkout()");

            var response = await this.FindUserCart();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel model)
        {
            _logger.LogInformation("Web.CartController.Checkout()");

            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.Checkout(model.CartHeader, token);

            //if (response != null && response.GetType() == typeof(string))
            //{
            //    TempData["Error"] = response;
            //    return RedirectToAction(nameof(Checkout));
            //}
            //else if (response != null)
            //{
            //    return RedirectToAction(nameof(Confirmation));
            //}
           
            if (response != null)
            {
                if (response.GetType() == typeof(string))
                {
                    TempData["Error"] = response;
                    return RedirectToAction(nameof(Checkout));
                }

                return RedirectToAction(nameof(Confirmation));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            _logger.LogInformation("Web.CartController.Confirmation()");

            return View();
        }

        private async Task<CartViewModel> FindUserCart()
        {
            _logger.LogInformation("Web.CartController.FindUserCart()");

            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.FindCartByUserId(userId, token);

            if (response?.CartHeader != null)
            {
                response.CartHeader.DiscountAmount = 0.0M;
                if (!string.IsNullOrEmpty(response.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCoupon(response.CartHeader.CouponCode, token);
                    if (coupon?.CouponCode != null)
                        response.CartHeader.DiscountAmount = coupon.DiscountAmount;
                }

                response.CartHeader.PurchaseAmount = 0.0M;
                foreach (var detail in response.CartDetails)
                {
                    response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }

                response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
            }

            return response;
        }
    }
}
