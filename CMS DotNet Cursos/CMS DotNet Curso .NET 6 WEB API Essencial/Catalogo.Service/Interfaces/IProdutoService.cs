using Catalogo.Domain.Models;

namespace Catalogo.Service.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto> GetByIdAsync(Guid id);
    Task<Produto> InsertAsync(Produto input);
    Task<Produto> UpdateAsync(Guid id, Produto input);
    Task<bool> DeleteAsync(Guid id);
}
