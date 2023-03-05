namespace AwesomeDevEvents.API.Persistence.Interfaces
{
    public interface IUnitofWork
    {
        Task<bool> CommitAsync();
        Task RollbackAsync();
    }
}
