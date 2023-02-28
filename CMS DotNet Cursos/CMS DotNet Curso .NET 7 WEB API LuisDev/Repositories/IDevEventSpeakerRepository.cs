using AwesomeDevEvents.API.DTOs;

namespace AwesomeDevEvents.API.Repositories
{
    public interface IDevEventSpeakerRepository
    {
        Task<IEnumerable<DevEventSpeakerDTO>> FindAll();
        Task<DevEventSpeakerDTO> FindById(Guid id);
        Task<DevEventSpeakerDTO> Create(DevEventSpeakerDTO vo);
        Task<DevEventSpeakerDTO> Update(DevEventSpeakerDTO vo);
        Task<bool> Delete(Guid id);
    }
}
