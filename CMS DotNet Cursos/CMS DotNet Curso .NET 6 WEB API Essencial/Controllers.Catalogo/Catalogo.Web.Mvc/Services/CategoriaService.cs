using Catalogo.Web.Mvc.Models;
using Catalogo.Web.Mvc.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Catalogo.Web.Mvc.Services;

public class CategoriaService : ICategoriaService
{
    private const string _apiEndpoint = "/api/1/categorias/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    public CategoriaService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<CategoriaViewModel>?> GetAllAsync()
    {
        var client = _clientFactory.CreateClient("CategoriasApi");

        using var response = await client.GetAsync(_apiEndpoint);

        if (response.IsSuccessStatusCode) // status code entre 200-299
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            // IEnumerable<CategoriaViewModel>? categoriasVM = new List<CategoriaViewModel>();
            var categoriasVM = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _options);
            return categoriasVM;
        }

        return null;
    }

    public async Task<CategoriaViewModel?> GetByIdAsync(int id)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");

        using var response = await client.GetAsync(_apiEndpoint + id);

        if (response.IsSuccessStatusCode) // status code entre 200-299
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            var categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _options);
            return categoriaVM;
        }

        return null;
    }

    public async Task<CategoriaViewModel?> CreateAsync(CategoriaViewModel? model)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");

        var categoria = JsonSerializer.Serialize(model);
        var content = new StringContent(categoria, Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(_apiEndpoint, content);

        if (response.IsSuccessStatusCode) // status code entre 200-299
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            var categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _options);
            return categoriaVM;
        }

        return null;
    }

    public async Task<bool> UpdateAsync(int id, CategoriaViewModel model)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");

        using var response = await client.PutAsJsonAsync(_apiEndpoint + id, model);

        if (response.IsSuccessStatusCode) // status code entre 200-299
            return true;

        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");

        using var response = await client.DeleteAsync(_apiEndpoint + id);

        if (response.IsSuccessStatusCode) // status code entre 200-299
            return true;

        return false;
    }
}
