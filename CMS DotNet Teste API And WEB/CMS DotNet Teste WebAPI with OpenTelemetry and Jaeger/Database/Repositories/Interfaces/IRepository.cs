using Microsoft.EntityFrameworkCore.Storage;

namespace Project.Database.Repositories.Interfaces;

public partial interface IRepository : IDisposable, IAsyncDisposable
{
    Task SaveChangesAsync();
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void SaveChanges();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task CommitAsync();
    Task RollbackAsync();
}