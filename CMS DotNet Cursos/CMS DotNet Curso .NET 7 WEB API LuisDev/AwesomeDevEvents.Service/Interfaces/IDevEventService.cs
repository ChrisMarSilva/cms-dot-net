using AwesomeDevEvents.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeDevEvents.Service.Interfaces
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
