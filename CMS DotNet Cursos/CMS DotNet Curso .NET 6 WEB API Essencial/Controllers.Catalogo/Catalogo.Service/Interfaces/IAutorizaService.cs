using Catalogo.Domain.Dtos;

namespace Catalogo.Service.Interfaces;

public interface IAutorizaService
{
    Task<UsuarioResponseDTO> LoginAsync(LoginRequestDTO request);
    Task<UsuarioResponseDTO> RegisterAsync(UsuarioRequestDTO request);
    Task LogoutAsync();
}
