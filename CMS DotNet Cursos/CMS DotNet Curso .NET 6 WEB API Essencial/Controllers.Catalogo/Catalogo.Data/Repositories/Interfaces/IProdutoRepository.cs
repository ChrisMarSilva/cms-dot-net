using Catalogo.Domain.Models;

namespace Catalogo.Data.Repositories.Interfaces;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorPreco();
    Task<IEnumerable<Produto>> FindAllAsync();
    //Task<Produto> GetByIdAsync(Guid id);
    //Task<Produto> AddAsync(Produto input);
    //Produto Update(Produto input);
    //bool Remove(Produto input);
}
