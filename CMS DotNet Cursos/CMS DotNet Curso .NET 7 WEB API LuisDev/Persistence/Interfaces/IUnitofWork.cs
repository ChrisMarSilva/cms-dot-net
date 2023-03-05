namespace AwesomeDevEvents.API.Persistence.Interfaces
{
    public interface IUnitofWork
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
