using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Extensions.Logging;

namespace GeekShopping.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly ILogger<CouponService> _logger;
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/coupon";

        public CouponService(
            ILogger<CouponService> logger,
            HttpClient client
            )
        {
            _logger = logger;
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger.LogInformation("Web.ProductService");
        }

        public async Task<CouponViewModel> GetCoupon(string code, string token)
        {
            _logger.LogInformation("Web.CartService.FindCartByUserId()");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{code}");

            if (response.StatusCode != HttpStatusCode.OK) 
                return new CouponViewModel();

            var resultado = await response.ReadContentAs<CouponViewModel>();
            return resultado;
        }
    }
}
