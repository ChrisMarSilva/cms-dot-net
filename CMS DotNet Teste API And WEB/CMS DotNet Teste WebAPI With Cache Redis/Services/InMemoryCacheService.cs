using Microsoft.Extensions.Caching.Memory;

namespace WebApplication1.Services
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private static MemoryCacheEntryOptions _cacheEntryOptions;

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(100)) // 100 // 40
                .SetSlidingExpiration(TimeSpan.FromSeconds(100)); // 100 // 10
        }

        public Task<string> GetCacheValueAsync(string key)
        {
            //    //if (!_memoryCache.TryGetValue(key, out string value))
            //    //    return "";
            //    //return value;
            throw new NotImplementedException();
        }

        public Task SetCacheValueAsync(string key, string value)
        {
            //    // _memoryCache.Set(key, value, MemoryCacheEntryOptions(){ AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(40), SlidingExpiration = TimeSpan.FromSeconds(10)});
            //    _memoryCache.Set(key, value, _cacheEntryOptions);
            throw new NotImplementedException();
        }

    }
}
