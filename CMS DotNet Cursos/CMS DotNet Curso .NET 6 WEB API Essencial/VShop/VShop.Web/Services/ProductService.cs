using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services;

public class ProductService : IProductService
{
    private const string apiEndpoint = "/api/products/";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
    }

    public async Task<IEnumerable<ProductViewModel>?> GetAllProductsAsync(string token)
    {
        var client = _clientFactory.CreateClient(name: "ProductApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.GetAsync(requestUri: apiEndpoint);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var productsVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(utf8Json: apiResponse, options: _options);
        return productsVM;
    }

    public async Task<ProductViewModel?> FindProductByIdAsync(int id, string token)
    {
        var client = _clientFactory.CreateClient(name: "ProductApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.GetAsync(requestUri: apiEndpoint + id);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(utf8Json: apiResponse, options: _options);
        return productVM;
    }

    public async Task<ProductViewModel?> CreateProductAsync(ProductViewModel product, string token)
    {
        var client = _clientFactory.CreateClient(name: "ProductApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        var content = new StringContent(content: JsonSerializer.Serialize(product), encoding: Encoding.UTF8, mediaType: "application/json");

        using var response = await client.PostAsync(requestUri: apiEndpoint, content: content);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(utf8Json: apiResponse, options: _options);
        return productVM;
    }

    public async Task<ProductViewModel?> UpdateProductAsync(ProductViewModel productVM, string token)
    {
        var client = _clientFactory.CreateClient(name: "ProductApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.PutAsJsonAsync(requestUri: apiEndpoint, value: productVM);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var productUpdated = await JsonSerializer.DeserializeAsync<ProductViewModel>(utf8Json: apiResponse, options: _options);
        return productUpdated;
    }

    public async Task<bool> DeleteProductByIdAsync(int id, string token)
    {
        var client = _clientFactory.CreateClient(name: "ProductApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.DeleteAsync(requestUri: apiEndpoint + id);

        return response.IsSuccessStatusCode;
    }
}
