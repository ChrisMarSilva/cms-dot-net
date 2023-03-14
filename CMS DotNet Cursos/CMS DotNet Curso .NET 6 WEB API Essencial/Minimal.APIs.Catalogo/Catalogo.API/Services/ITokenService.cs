using Catalogo.API.Models;

namespace Catalogo.API.Services;

public interface ITokenService
{
    string GerarToken(string key, string issuer, string audience, UserModel user);
}

