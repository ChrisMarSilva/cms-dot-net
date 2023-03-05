using AwesomeDevEvents.API.Models;
using AwesomeDevEvents.API.ViewModels;

namespace AwesomeDevEvents.API.Repositories.Interfaces
{
    public interface IDevEventRepository
    {
        Task<IEnumerable<DevEventOutput>> FindAllAsync();
        Task<DevEventOutput> FindByIdAsync(Guid id);
        Task<DevEvent> FindByIdSimpleAsync(Guid id);
        Task<bool> FindAnyAsync(Guid id);
        Task<DevEventOutput> CreateAsync(DevEventInput input);
        Task<DevEventOutput> UpdateAsync(DevEvent input);
        Task<bool> DeleteAsync(Guid id);
    }
}
