using Catalogo.Data.Persistence.Interfaces;
using Catalogo.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogo.Data.Persistence;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ILogger<UnitOfWork> _logger;
    private readonly AppDbContext _ctx;
    private bool _disposed;

    public IProdutoRepository Produtos { get; private set; }
    public ICategoriaRepository Categorias { get; private set; }

    public UnitOfWork(
        ILogger<UnitOfWork> logger,
        AppDbContext ctx,
        IProdutoRepository prodRepo,
        ICategoriaRepository categRepo
        )
    {
        _logger = logger;
        _ctx = ctx;
        Produtos = prodRepo ?? throw new ArgumentNullException(nameof(IProdutoRepository));
        Categorias = categRepo ?? throw new ArgumentNullException(nameof(ICategoriaRepository));
    }

    // public DbContext Context => _ctx;
    // public IProdutoRepository Produtos { get { return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_ctx); } }
    // public ICategoriaRepository Categorias { get { return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_ctx); } }

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