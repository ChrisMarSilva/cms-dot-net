
namespace Catalogo.Domain.Dtos;

public class UsuarioResponseDTO
{
    public bool Authenticated { get; set; }
    public DateTime Expiration { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    //public string RefreshToken { get; set; }
    //public DateTime RefreshTokenExpiration { get; set; }
}
