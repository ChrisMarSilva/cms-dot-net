using Catalogo.Domain.Models;

namespace Catalogo.Data.Repositories.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> FindAllAsync();
    Task<Produto> FindByIdAsync(Guid id);
    Task<Produto> CreateAsync(Produto input);
    Produto Update(Produto input);
    bool Delete(Produto input);
}
