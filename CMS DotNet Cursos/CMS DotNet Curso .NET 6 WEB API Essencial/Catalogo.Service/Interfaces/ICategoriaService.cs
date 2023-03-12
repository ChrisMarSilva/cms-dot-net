using Catalogo.Domain.Models;

namespace Catalogo.Service.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria> GetByIdAsync(Guid id);
    Task<Categoria> InsertAsync(Categoria input);
    Task<Categoria> UpdateAsync(Guid id, Categoria input);
    Task<bool> DeleteAsync(Guid id);
}
