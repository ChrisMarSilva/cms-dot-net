using Catalogo.Domain.Models;
using Catalogo.Domain.Pagination;

namespace Catalogo.Domain.Interfaces;

public interface ICategoriaRepository : IBaseRepository<Categoria>
{
    Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categParams);
    Task<IEnumerable<Categoria>> GetAllAsync();
    //Task<Categoria> GetByIdAsync(Guid id);
    //Task<Categoria> AddAsync(Categoria input);
    //Categoria Update(Categoria input);
    //bool Remove(Categoria input);
}
