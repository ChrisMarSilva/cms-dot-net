using Catalogo.Domain.Interfaces;

namespace Catalogo.Infrastructure.Context.Interfaces;

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
