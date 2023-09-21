using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services;

public class CartService : ICartService
{
    private const string apiEndpoint = "/api/cart";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions? _options;

    public CartService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
    }

    public async Task<CartViewModel?> GetCartByUserIdAsync(string userId, string token)
    {
        var client = _clientFactory.CreateClient(name: "CartApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.GetAsync(requestUri: $"{apiEndpoint}/getcart/{userId}");

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var cartVM = await JsonSerializer.DeserializeAsync<CartViewModel>(utf8Json: apiResponse, options: _options);
        return cartVM;
    }

    public async Task<CartViewModel?> AddItemToCartAsync(CartViewModel cart, string token)
    {
        var client = _clientFactory.CreateClient(name: "CartApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        var content = new StringContent(JsonSerializer.Serialize(cart), Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(requestUri: $"{apiEndpoint}/addcart/", content: content);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var cartVM = await JsonSerializer.DeserializeAsync<CartViewModel>(utf8Json: apiResponse, options: _options);
        return cartVM;
    }

    public async Task<CartViewModel?> UpdateCartAsync(CartViewModel cartVM, string token)
    {
        var client = _clientFactory.CreateClient(name: "CartApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.PutAsJsonAsync(requestUri: $"{apiEndpoint}/updatecart/", value: cartVM);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var cartUpdated = await JsonSerializer.DeserializeAsync<CartViewModel>(utf8Json: apiResponse, options: _options);
        return cartUpdated;
    }

    public async Task<bool> RemoveItemFromCartAsync(int cartId, string token)
    {
        var client = _clientFactory.CreateClient(name: "CartApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.DeleteAsync(requestUri: $"{apiEndpoint}/deletecart/" + cartId);

        return response.IsSuccessStatusCode;
    }

    public Task<bool> ClearCartAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ApplyCouponAsync(CartViewModel cartVM, string token)
    {
        var client = _clientFactory.CreateClient(name: "CartApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        var content = new StringContent(JsonSerializer.Serialize(cartVM), Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(requestUri: $"{apiEndpoint}/applycoupon/", content: content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveCouponAsync(string userId, string token)
    {
        var client = _clientFactory.CreateClient(name: "CartApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.DeleteAsync(requestUri: $"{apiEndpoint}/deletecoupon/{userId}");

        return response.IsSuccessStatusCode;
    }

    public async Task<CartHeaderViewModel?> CheckoutAsync(CartHeaderViewModel cartHeader, string token)
    {
        var client = _clientFactory.CreateClient(name: "CartApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        var content = new StringContent(JsonSerializer.Serialize(cartHeader), Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(requestUri: $"{apiEndpoint}/checkout/", content: content);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var cartHeaderVM = await JsonSerializer.DeserializeAsync<CartHeaderViewModel>(utf8Json: apiResponse, options: _options);
        return cartHeaderVM;
    }
}
