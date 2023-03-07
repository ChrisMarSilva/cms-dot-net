using AwesomeDevEvents.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AwesomeDevEvents.Service
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache; //  = MemoryCache.Default;

        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_cache.Get(key);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _cache.Set(key, value, expirationTime);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public bool RemoveData(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _cache.Remove(key);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
