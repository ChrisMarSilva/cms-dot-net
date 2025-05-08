using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace Cache.Infra.Data.Context;

public interface IUnitOfWork : IDisposable
{
    // IBaseRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
    void Commit();
    Task<bool> CommitAsync();
    void Rollback();
    Task<bool> RollbackAsync();
    int SaveChanges(bool ensureAutoHistory = false, Transaction transaction = null);
    Task<int> SaveChangesAsync(bool ensureAutoHistory = false, Transaction transaction = null, CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
}