﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/cart";

        public CartService(
            ILogger<CartService> logger, 
            HttpClient client
            )
        {
            _logger = logger;
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger.LogInformation("Web.ProductService");
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _logger.LogInformation("Web.CartService.FindCartByUserId()");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/find-cart/{userId}");

            var resultado = await response.ReadContentAs<CartViewModel>();
            return resultado;
        }

        public async Task<CartViewModel> AddItemToCart(CartViewModel model, string token)
        {
            _logger.LogInformation("Web.CartService.AddItemToCart()");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BasePath}/add-cart", model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");

            var resultado = await response.ReadContentAs<CartViewModel>();
            return resultado;
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel model, string token)
        {
            _logger.LogInformation("Web.CartService.UpdateCart()");
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson($"{BasePath}/update-cart", model);
            
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");

            var resultado = await response.ReadContentAs<CartViewModel>();
            return resultado;
        }

        public async Task<bool> RemoveFromCart(long cartId, string token)
        {
            _logger.LogInformation("Web.CartService.RemoveFromCart()");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/remove-cart/{cartId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");

            var resultado = await response.ReadContentAs<bool>();
            return resultado;
        }

        public async Task<bool> ApplyCoupon(CartViewModel model, string token)
        {
            _logger.LogInformation("Web.CartService.ApplyCoupon()");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BasePath}/apply-coupon", model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");

            var resultado = await response.ReadContentAs<bool>();
            return resultado;
        }

        public async Task<bool> RemoveCoupon(string userId, string token)
        {
            _logger.LogInformation("Web.CartService.RemoveCoupon()");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/remove-coupon/{userId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");

            var resultado = await response.ReadContentAs<bool>();
            return resultado;
        }

        public async Task<object> Checkout(CartHeaderViewModel model, string token)
        {
            _logger.LogInformation("Web.CartService.Checkout()");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BasePath}/checkout", model);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode.ToString().Equals("PreconditionFailed"))
                    return "Coupon Price has changed, please confirm!";
                throw new Exception("Something went wrong when calling API");
            }

            var resultado = await response.ReadContentAs<CartHeaderViewModel>();
            return resultado;
        }

        public async Task<bool> ClearCart(string userId, string token)
        {
            _logger.LogInformation("Web.CartService.ClearCart()");
            throw new NotImplementedException();
        }
    }
}
