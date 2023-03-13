using Catalogo.Domain.Models;

namespace Catalogo.Data.Repositories.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> FindAllAsync();
    Task<Categoria> FindByIdAsync(Guid id);
    Task<Categoria> CreateAsync(Categoria input);
    Categoria Update(Categoria input);
    bool Delete(Categoria input);
}
