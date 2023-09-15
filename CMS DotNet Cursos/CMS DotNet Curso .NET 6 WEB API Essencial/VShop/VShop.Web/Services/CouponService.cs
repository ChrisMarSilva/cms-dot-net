using System.Net.Http.Headers;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services;

public class CouponService : ICouponService
{
    private const string apiEndpoint = "/api/coupon";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions? _options;

    public CouponService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
    }

    public async Task<CouponViewModel?> GetDiscountCouponAsync(string couponCode, string token)
    {
        var client = _clientFactory.CreateClient(name: "DiscountApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.GetAsync(requestUri: $"{apiEndpoint}/{couponCode}");

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var couponVM = await JsonSerializer.DeserializeAsync<CouponViewModel>(utf8Json: apiResponse, options: _options);
        return couponVM;
    }
}
