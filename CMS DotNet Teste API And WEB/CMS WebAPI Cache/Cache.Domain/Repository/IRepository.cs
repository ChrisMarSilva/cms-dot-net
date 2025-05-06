namespace Cache.Domain.Repository;

public interface IRepository
{
    Task<IRepositoryTransaction> BeginTransactionAsync();

    Task SaveChangesAsync();

    void RemoveTrack<T>(T entity) where T : class;
}