using Catalogo.Data.Repositories.Interfaces;

namespace Catalogo.Data.Persistence.Interfaces;

public interface IUnitOfWork
{
    IProdutoRepository Produtos { get; }
    ICategoriaRepository Categorias { get; }
    void Commit();
    Task<bool> CommitAsync();
    Task RollbackAsync();
}
