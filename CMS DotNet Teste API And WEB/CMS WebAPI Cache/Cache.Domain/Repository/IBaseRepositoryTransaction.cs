namespace Cache.Domain.Repository;

public interface IBaseRepositoryTransaction : IDisposable, IAsyncDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}