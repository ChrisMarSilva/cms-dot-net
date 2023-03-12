using Tarefas.Data.Persistence.Interfaces;

namespace Tarefas.Data.Persistence;

public class UnitOfWork : IUnitofWork, IDisposable
{
    private AppDbContext _context; // _context  // _ctx
    private bool _disposed;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public Task RollbackAsync()
    {
        return null;
    }

    protected virtual void Dispose(bool disposing)
    {
        //if (!this.disposed)
        //{
        //    if (disposing)
        //    {
        //        _context.Dispose();
        //    }
        //}
        if (!_disposed && disposing)
            _context.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}