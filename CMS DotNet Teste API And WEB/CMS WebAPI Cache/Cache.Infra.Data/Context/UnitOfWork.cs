using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Transactions;

namespace Cache.Infra.Data.Context;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;
    private Dictionary<Type, object> _repositories;
    private bool _disposed;

    public UnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    //public IBaseRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
    //{
    //    _repositories ??= [];

    //    if (hasCustomRepository)
    //        return _context.GetService<IBaseRepository<TEntity>>();

    //    var type = typeof(TEntity);
    //    if (_repositories.TryGetValue(type, out var value))
    //        return (IBaseRepository<TEntity>)value;

    //    value = new BaseRepository<TEntity>(_context, main);
    //    _repositories[type] = value;

    //    return (IBaseRepository<TEntity>)value;
    //}

    public void Commit() =>
            _context.SaveChanges();

    public async Task<bool> CommitAsync() => 
        await _context.SaveChangesAsync() > 0;

    public void Rollback() { }

    public async Task<bool> RollbackAsync() 
        => true;

    public int SaveChanges(bool ensureAutoHistory = false, Transaction transaction = null)
    {
        if (transaction != null)
        {
            if (_context.Database.GetDbConnection().State != ConnectionState.Open)
            {
                _context.Database.OpenConnection();
            }

            _context.Database.EnlistTransaction(transaction);
        }

        if (ensureAutoHistory)
        {
            //_context.EnsureAutoHistory();
        }

        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, Transaction transaction = null, CancellationToken cancellationToken = default)
    {
        if (transaction != null)
        {
            if (_context.Database.GetDbConnection().State != ConnectionState.Open)
            {
                await _context.Database.OpenConnectionAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            }

            _context.Database.EnlistTransaction(transaction);
        }

        if (ensureAutoHistory)
        {
            //_context.EnsureAutoHistory();
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
    {
        using var ts = new TransactionScope();
        var count = 0;
        foreach (var unitOfWork in unitOfWorks)
        {
            count += await unitOfWork.SaveChangesAsync(ensureAutoHistory);
        }

        count += await SaveChangesAsync(ensureAutoHistory);

        ts.Complete();

        return count;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
        await _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel, CancellationToken cancellationToken = default) =>
        await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

    protected virtual void Dispose(bool disposing)
    {
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