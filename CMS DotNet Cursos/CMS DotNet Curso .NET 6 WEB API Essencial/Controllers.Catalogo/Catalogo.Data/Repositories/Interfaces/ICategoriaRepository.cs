using Catalogo.Domain.Models;

namespace Catalogo.Data.Repositories.Interfaces;

public interface ICategoriaRepository : IBaseRepository<Categoria>
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    //Task<Categoria> GetByIdAsync(Guid id);
    //Task<Categoria> AddAsync(Categoria input);
    //Categoria Update(Categoria input);
    //bool Remove(Categoria input);
}
