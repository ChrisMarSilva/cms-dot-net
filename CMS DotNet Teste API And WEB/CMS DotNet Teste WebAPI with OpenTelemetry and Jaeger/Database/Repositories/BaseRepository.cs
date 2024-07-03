using Microsoft.EntityFrameworkCore.Storage;
using Project.Database.Context;
using Project.Database.Repositories.Interfaces;

namespace Project.Database.Repositories;

internal class BaseRepository : IRepository
{
    protected readonly ApplicationDbContext _context;
    protected IDbContextTransaction? Transaction;
    private bool _disposedValue;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        Transaction = await _context.Database.BeginTransactionAsync();
        return Transaction;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        Transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return Transaction;
    }

    public async Task CommitAsync()
    {
        ArgumentNullException.ThrowIfNull(Transaction, "_transaction != null");
        await Transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        ArgumentNullException.ThrowIfNull(Transaction, "_transaction != null");
        await Transaction.RollbackAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing) Transaction?.Dispose();
        _disposedValue = true;
    }

    private async Task DisposeAsync(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing) if (Transaction is not null) await Transaction.DisposeAsync();
        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
