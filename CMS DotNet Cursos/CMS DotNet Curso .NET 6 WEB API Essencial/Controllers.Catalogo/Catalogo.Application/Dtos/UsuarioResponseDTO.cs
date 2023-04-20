
using System.Text.Json.Serialization;

namespace Catalogo.Application.Dtos;

public class UsuarioResponseDTO
{
    [property: JsonPropertyName("authenticated")] public bool Authenticated { get; set; }
    [property: JsonPropertyName("expiration")] public DateTime Expiration { get; set; }
    [property: JsonPropertyName("token")] public string Token { get; set; } = string.Empty;
    [property: JsonPropertyName("message")] public string Message { get; set; } = string.Empty;
    //[property: JsonPropertyName("refreshtoken")] public string RefreshToken { get; set; }
    //[property: JsonPropertyName("refreshtokenexpiration")] public DateTime RefreshTokenExpiration { get; set; }
}
