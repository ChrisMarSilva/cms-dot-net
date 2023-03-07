using AwesomeDevEvents.Domain.Models;

namespace AwesomeDevEvents.Infrastructure.Providers.Interfaces
{
    public interface ICacheProvider
    {
        Task<IEnumerable<DevEvent>> GetCachedResponse();
    }
}
