using Catalogo.Web.Mvc.Models;

namespace Catalogo.Web.Mvc.Services.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaViewModel>?> GetAllAsync();
    Task<CategoriaViewModel?> GetByIdAsync(int id);
    Task<CategoriaViewModel?> CreateAsync(CategoriaViewModel? categoriaVM);
    Task<bool> UpdateAsync(int id, CategoriaViewModel categoriaVM);
    Task<bool> DeleteAsync(int id);
}
