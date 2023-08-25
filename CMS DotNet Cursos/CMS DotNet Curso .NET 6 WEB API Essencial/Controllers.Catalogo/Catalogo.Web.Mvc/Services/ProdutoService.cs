using Catalogo.Web.Mvc.Models;
using Catalogo.Web.Mvc.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Catalogo.Web.Mvc.Services;

public class ProdutoService : IProdutoService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/1/produtos/";
    private readonly JsonSerializerOptions _options;

    public ProdutoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProdutoViewModel>?> GetAllAsync(string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.GetAsync(apiEndpoint);

        if (response.IsSuccessStatusCode) // status code entre 200-299
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            var produtosVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>?>(apiResponse, _options);
            return produtosVM;
        }

        return null;
    }

    public async Task<ProdutoViewModel?> GetByIdAsync(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.GetAsync(apiEndpoint + id);

        if (response.IsSuccessStatusCode) // status code entre 200-299
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            var produtoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel?>(apiResponse, _options);
            return produtoVM;
        }

        return null;
    }

    public async Task<ProdutoViewModel?> CreateAsync(ProdutoViewModel? model, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        var produto = JsonSerializer.Serialize(model);
        var content = new StringContent(produto, Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(apiEndpoint, content);

        if (response.IsSuccessStatusCode) // status code entre 200-299
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            var produtoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel?>(apiResponse, _options);
            return produtoVM;
        }

        return null;
    }

    public async Task<bool> UpdateAsync(int id, ProdutoViewModel model, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.PutAsJsonAsync(apiEndpoint + id, model);

        if (response.IsSuccessStatusCode) // status code entre 200-299
            return true;

        return false;
    }

    public async Task<bool> DeleteAsync(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token: token, client: client);

        using var response = await client.DeleteAsync(apiEndpoint + id);

        if (response.IsSuccessStatusCode) // status code entre 200-299
            return true;

        return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
