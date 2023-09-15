using System.Net.Http.Headers;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services;

public class CategoryService : ICategoryService
{
    private const string apiEndpoint = "/api/categories/";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;

    public CategoryService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
    }

    public async Task<IEnumerable<CategoryViewModel>?> GetAllCategoriesAsync(string token)
    {
        var client = _clientFactory.CreateClient(name: "ProductApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.GetAsync(requestUri: apiEndpoint);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadAsStreamAsync();

        var categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(utf8Json: apiResponse, options: _options);
        return categories;
    }
}

