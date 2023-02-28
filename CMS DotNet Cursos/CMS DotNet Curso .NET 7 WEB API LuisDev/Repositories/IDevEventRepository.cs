using AwesomeDevEvents.API.DTOs;

namespace AwesomeDevEvents.API.Repositories
{
    public interface IDevEventRepository
    {
        Task<IEnumerable<DevEventDTO>> FindAll();
        Task<DevEventDTO> FindById(Guid id);
        Task<DevEventDTO> Create(DevEventDTO vo);
        Task<DevEventDTO> Update(DevEventDTO vo);
        Task<bool> Delete(Guid id);
    }
}
