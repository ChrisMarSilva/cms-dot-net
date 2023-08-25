using Catalogo.Web.Mvc.Models;
using Catalogo.Web.Mvc.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Catalogo.Web.Mvc.Services;

public class AutenticacaoService : IAutenticacaoService
{
    const string apiEndpointAutentica = "/api/autoriza/login/";
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;

    public AutenticacaoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<TokenViewModel?> AutenticaUsuarioAsync(UsuarioViewModel model)
    {
        var client = _clientFactory.CreateClient("AutenticaApi");

        var usuario = JsonSerializer.Serialize(model);
        var content = new StringContent(usuario, Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(apiEndpointAutentica, content);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            var tokenUsuario = await JsonSerializer.DeserializeAsync<TokenViewModel>(apiResponse, _options);
            return tokenUsuario;
        }

        return null;
    }
}
