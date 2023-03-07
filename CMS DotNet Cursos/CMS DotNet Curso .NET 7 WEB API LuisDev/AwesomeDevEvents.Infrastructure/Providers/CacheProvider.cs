using AwesomeDevEvents.Domain.Models;
using AwesomeDevEvents.Infrastructure.Providers.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AwesomeDevEvents.Infrastructure.Providers
{
    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
       
        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<DevEvent>> GetCachedResponse()
        {
            try
            {
                return await this.GetCachedResponse(CacheKeys.DevEvents, GetUsersSemaphore);
            }
            catch
            {
                throw;
            }
        }

        private async Task<IEnumerable<DevEvent>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore)
        {
            bool isAvaiable = _cache.TryGetValue(cacheKey, out List<DevEvent> devEvents);
            if (isAvaiable) 
                return devEvents;
           
            try
            {
                await semaphore.WaitAsync();

                isAvaiable = _cache.TryGetValue(cacheKey, out devEvents);
                if (isAvaiable) 
                    return devEvents;

                devEvents = null; // EmployeeService.GetEmployeesDeatilsFromDB();
                
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromSeconds(60),
                    Priority = CacheItemPriority.Normal,
                    Size = 1024,
                };

                _cache.Set(cacheKey, devEvents, cacheEntryOptions);
            }
            catch
            {
                throw;
            }
            finally
            {
                semaphore.Release();
            }

            return devEvents;
        }

    }
}
