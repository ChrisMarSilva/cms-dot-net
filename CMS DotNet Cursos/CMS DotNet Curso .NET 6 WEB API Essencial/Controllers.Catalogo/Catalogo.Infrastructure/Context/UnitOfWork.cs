using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogo.Infrastructure.Context;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ILogger<UnitOfWork> _logger;
    private readonly AppDbContext _ctx;
    private bool _disposed;

    public IProdutoRepository Produtos { get; private set; }
    public ICategoriaRepository Categorias { get; private set; }
    public IAlunoRepository Alunos { get; private set; }

    public UnitOfWork(
        AppDbContext ctx,
        IProdutoRepository prodRepo,
        ICategoriaRepository categRepo
        )
    {
        _ctx = ctx;
        Produtos = prodRepo ?? throw new ArgumentNullException(nameof(IProdutoRepository));
        Categorias = categRepo ?? throw new ArgumentNullException(nameof(ICategoriaRepository));
        //Alunos = alunoRepo ?? throw new ArgumentNullException(nameof(IAlunoRepository));
    }

    public UnitOfWork(
        ILogger<UnitOfWork> logger,
        AppDbContext ctx,
        IProdutoRepository prodRepo,
        ICategoriaRepository categRepo,
        IAlunoRepository alunoRepo
        )
    {
        _logger = logger;
        _ctx = ctx;
        Produtos = prodRepo ?? throw new ArgumentNullException(nameof(IProdutoRepository));
        Categorias = categRepo ?? throw new ArgumentNullException(nameof(ICategoriaRepository));
        Alunos = alunoRepo ?? throw new ArgumentNullException(nameof(IAlunoRepository));
    }

    // public DbContext Context => _ctx;
    // public IProdutoRepository Produtos { get { return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_ctx); } }
    // public ICategoriaRepository Categorias { get { return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_ctx); } }
    // public IAlunoRepository Alunos { get { return _alunoRepo = _alunoRepo ?? new AlunoRepository(_ctx); } }

    public void Commit() => _ctx.SaveChanges();
    public void Rollback() { }
    public async Task<bool> CommitAsync() => await _ctx.SaveChangesAsync() > 0;
    public async Task<bool> RollbackAsync() => true;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _ctx.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}