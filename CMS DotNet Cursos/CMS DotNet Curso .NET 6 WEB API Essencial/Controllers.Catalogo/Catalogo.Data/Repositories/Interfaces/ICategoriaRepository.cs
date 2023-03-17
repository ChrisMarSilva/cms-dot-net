using Catalogo.Domain.Models;

namespace Catalogo.Data.Repositories.Interfaces;

public interface ICategoriaRepository : IBaseRepository<Categoria>
{
    IEnumerable<Categoria> GetCategoriasProdutos();
    Task<IEnumerable<Categoria>> FindAllAsync();
    //Task<Categoria> GetByIdAsync(Guid id);
    //Task<Categoria> AddAsync(Categoria input);
    //Categoria Update(Categoria input);
    //bool Remove(Categoria input);
}
