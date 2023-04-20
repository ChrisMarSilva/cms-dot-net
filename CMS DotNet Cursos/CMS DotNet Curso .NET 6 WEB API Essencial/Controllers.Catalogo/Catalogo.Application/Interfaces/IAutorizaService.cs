using Catalogo.Application.Dtos;

namespace Catalogo.Application.Interfaces;

public interface IAutorizaService
{
    Task<UsuarioResponseDTO> LoginAsync(LoginRequestDTO request);
    Task<UsuarioResponseDTO> RegisterAsync(UsuarioRequestDTO request);
    Task LogoutAsync();
}
