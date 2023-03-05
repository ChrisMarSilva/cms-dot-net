using AwesomeDevEvents.API.ViewModels;

namespace AwesomeDevEvents.API.Repositories.Interfaces
{
    public interface IDevEventSpeakerRepository
    {
        Task<IEnumerable<DevEventSpeakerOutput>> FindAllAsync();
        Task<DevEventSpeakerOutput> FindByIdAsync(Guid id);
        Task<DevEventSpeakerOutput> CreateAsync(DevEventSpeakerInput input);
        Task<DevEventSpeakerOutput> UpdateAsync(DevEventSpeakerInput input);
        Task<bool> DeleteAsync(Guid id);
    }
}
