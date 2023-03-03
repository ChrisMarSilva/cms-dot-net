using AwesomeDevEvents.API.DTOs;
using AwesomeDevEvents.API.Entities;

namespace AwesomeDevEvents.API.Repositories
{
    public interface IDevEventRepository
    {
        Task<IEnumerable<DevEventDTO>> FindAll();
        Task<DevEventDTO> FindById(Guid id);
        Task<DevEvent> FindByIdSimple(Guid id);
        Task<bool> FindAny(Guid id);
        Task<DevEventDTO> Create(DevEvent vo);
        Task<DevEventDTO> Update(DevEvent vo);
        Task<bool> Delete(Guid id);
    }
}
