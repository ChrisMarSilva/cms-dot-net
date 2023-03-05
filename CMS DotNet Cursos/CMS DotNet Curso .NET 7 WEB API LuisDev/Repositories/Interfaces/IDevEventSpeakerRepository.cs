using AwesomeDevEvents.API.Models.Dtos;

namespace AwesomeDevEvents.API.Repositories.Interfaces
{
    public interface IDevEventSpeakerRepository
    {
        Task<IEnumerable<DevEventSpeakerOutputDto>> FindAllAsync();
        Task<DevEventSpeakerOutputDto> FindByIdAsync(Guid id);
        Task<DevEventSpeakerOutputDto> CreateAsync(DevEventSpeakerInputDto input);
        Task<DevEventSpeakerOutputDto> UpdateAsync(DevEventSpeakerInputDto input);
        Task<bool> DeleteAsync(Guid id);
    }
}
