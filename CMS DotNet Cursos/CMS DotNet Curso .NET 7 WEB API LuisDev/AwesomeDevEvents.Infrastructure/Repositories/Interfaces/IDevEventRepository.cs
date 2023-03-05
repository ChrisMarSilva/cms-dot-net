using AwesomeDevEvents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeDevEvents.Infrastructure.Repositories.Interfaces
{
    public interface IDevEventRepository
    {
        Task<IEnumerable<DevEvent>> FindAllAsync();
        Task<DevEvent> FindByIdAsync(Guid id);
        Task<bool> FindAnyAsync(Guid id);
        Task<DevEvent> CreateAsync(DevEvent devEvent);
        DevEvent Update(DevEvent devEvent);
        bool Delete(DevEvent devEvent);
    }
}
