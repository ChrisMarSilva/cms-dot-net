using AwesomeDevEvents.API.DTOs;
using AwesomeDevEvents.API.Entities;

namespace AwesomeDevEvents.API.Repositories
{
    public interface IDevEventSpeakerRepository
    {
        Task<IEnumerable<DevEventSpeakerDTO>> FindAll();
        Task<DevEventSpeakerDTO> FindById(Guid id);
        Task<DevEventSpeakerDTO> Create(DevEventSpeaker vo);
        Task<DevEventSpeakerDTO> Update(DevEventSpeakerDTO vo);
        Task<bool> Delete(Guid id);
    }
}
