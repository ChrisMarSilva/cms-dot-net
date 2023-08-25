using Catalogo.Web.Mvc.Models;

namespace Catalogo.Web.Mvc.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>?> GetAllAsync(string token);
    Task<ProdutoViewModel?> GetByIdAsync(int id, string token);
    Task<ProdutoViewModel?> CreateAsync(ProdutoViewModel? model, string token);
    Task<bool> UpdateAsync(int id, ProdutoViewModel model, string token);
    Task<bool> DeleteAsync(int id, string token);
}
