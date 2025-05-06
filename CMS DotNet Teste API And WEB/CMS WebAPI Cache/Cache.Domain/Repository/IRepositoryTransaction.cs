namespace Cache.Domain.Repository;

public interface IRepositoryTransaction : IDisposable, IAsyncDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}
