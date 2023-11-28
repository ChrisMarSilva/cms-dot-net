using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CMS_DotNet_Teste_WebAPI_with_Redis.Extensions;

public static class Cache
{
    public static async Task SetRecordAsync<T>(IDistributedCache cache, string key, T vakue, TimeSpan? absolute = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = absolute ?? TimeSpan.FromSeconds(20),
            AbsoluteExpirationRelativeToNow = absolute ?? TimeSpan.FromMinutes(1),
        };

        var json = JsonSerializer.Serialize(vakue);

        await cache.SetStringAsync(key, json, options);
    }

    public static async Task<T?> GetRecordAsync<T>(IDistributedCache cache, string key)
    {
        var json = await cache.GetStringAsync(key);

        if (json == null)
            return default(T);

        var data = JsonSerializer.Deserialize<T>(json);

        return data;
    }
}
