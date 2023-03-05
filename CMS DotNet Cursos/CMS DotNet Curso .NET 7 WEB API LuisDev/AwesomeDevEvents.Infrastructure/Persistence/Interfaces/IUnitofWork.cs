using System.Threading.Tasks;

namespace AwesomeDevEvents.Infrastructure.Persistence.Interfaces
{
    public interface IUnitofWork
    {
        Task<bool> CommitAsync();
        Task RollbackAsync();
    }
}
