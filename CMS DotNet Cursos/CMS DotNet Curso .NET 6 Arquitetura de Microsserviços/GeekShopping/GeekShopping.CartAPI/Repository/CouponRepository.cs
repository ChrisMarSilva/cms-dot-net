using GeekShopping.CartAPI.Data.ValueObjects;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.CartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ILogger<CouponRepository> _logger;
        private readonly HttpClient _client;

        public CouponRepository(
            ILogger<CouponRepository> logger,
            HttpClient client
            )
        {
            _logger = logger;
            _client = client;
            _logger.LogInformation("CartAPI.CouponRepository");
        }

        public async Task<CouponVO> GetCoupon(string couponCode, string token)
        {
            _logger.LogInformation("CartAPI.CouponRepository.GetCoupon()");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"/api/v1/coupon/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK) 
                return new CouponVO();
            
            var coupon = JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return coupon;
        }
    }
}
