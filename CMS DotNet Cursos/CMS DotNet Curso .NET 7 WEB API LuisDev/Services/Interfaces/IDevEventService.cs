using AwesomeDevEvents.API.Models.Dtos;

namespace AwesomeDevEvents.API.Services.Interfaces
{
    public interface IDevEventService
    {
        Task<IEnumerable<DevEventOutputDto>> GetAllAsync();
        Task<DevEventOutputDto> GetByIdAsync(Guid id);
        Task<DevEventOutputDto> InsertAsync(DevEventInputDto input);
        Task<DevEventOutputDto> UpdateAsync(Guid id, DevEventInputDto input);
        Task<bool> DeleteAsync(Guid id);
    }
}
