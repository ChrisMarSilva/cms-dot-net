using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _ctx;
    private bool _disposed;

    public ITradeRepository Trades { get; private set; }

    public UnitOfWork(AppDbContext ctx, ITradeRepository prodRepo)
    {
        _ctx = ctx;
        Trades = prodRepo ?? throw new ArgumentNullException(nameof(ITradeRepository));
    }

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
