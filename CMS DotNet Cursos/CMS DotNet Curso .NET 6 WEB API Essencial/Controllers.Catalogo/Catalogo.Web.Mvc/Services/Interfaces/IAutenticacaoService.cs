using Catalogo.Web.Mvc.Models;

namespace Catalogo.Web.Mvc.Services.Interfaces;

public interface IAutenticacaoService

{
    Task<TokenViewModel?> AutenticaUsuarioAsync(UsuarioViewModel usuarioVM);
}