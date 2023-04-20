using Catalogo.Domain.Models;
using Catalogo.Domain.Pagination;

namespace Catalogo.Domain.Interfaces;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters prodParams);
    Task<IEnumerable<Produto>> GetAllAsync();
    //Task<Produto> GetByIdAsync(Guid id);
    //Task<Produto> AddAsync(Produto input);
    //Produto Update(Produto input);
    //bool Remove(Produto input);
}
