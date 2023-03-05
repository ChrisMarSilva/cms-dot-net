using AwesomeDevEvents.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeDevEvents.Infrastructure.Repositories.Interfaces
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
