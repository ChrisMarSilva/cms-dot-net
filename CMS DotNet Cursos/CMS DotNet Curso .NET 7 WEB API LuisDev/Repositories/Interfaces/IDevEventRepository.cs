using AwesomeDevEvents.API.Models.Entities;

namespace AwesomeDevEvents.API.Repositories.Interfaces
{
    public interface IDevEventRepository
    {
        Task<IEnumerable<DevEvent>> FindAllAsync();
        Task<DevEvent> FindByIdAsync(Guid id);
        Task<DevEvent> FindByIdSimpleAsync(Guid id);
        Task<bool> FindAnyAsync(Guid id);
        Task<DevEvent> CreateAsync(DevEvent devEvent);
        DevEvent Update(DevEvent devEvent);
        bool Delete(DevEvent devEvent);
    }
}
