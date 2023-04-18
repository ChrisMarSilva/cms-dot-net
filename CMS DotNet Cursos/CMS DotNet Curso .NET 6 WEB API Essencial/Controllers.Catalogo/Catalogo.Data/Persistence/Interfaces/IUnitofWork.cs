using Catalogo.Data.Repositories.Interfaces;

namespace Catalogo.Data.Persistence.Interfaces;

public interface IUnitOfWork
{
    IProdutoRepository Produtos { get; }
    ICategoriaRepository Categorias { get; }
    IAlunoRepository Alunos { get; }
    void Commit();
    void Rollback();
    Task<bool> CommitAsync();
    Task<bool> RollbackAsync();
}
