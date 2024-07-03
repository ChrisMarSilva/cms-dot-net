using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Project.Extensions;

public static class DistributedCacheExtensions
{
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);

    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    public static async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> factory, DistributedCacheEntryOptions? cacheOptions = null)
    {
        var cachedData = await cache.GetStringAsync(key);

        if (cachedData is not null)
            return JsonSerializer.Deserialize<T>(cachedData);

        var data = await factory();

        try
        {
            await Semaphore.WaitAsync();

            await cache.SetStringAsync(key, JsonSerializer.Serialize(data), cacheOptions ?? DefaultExpiration);
        }
        finally
        {
            Semaphore.Release();
        }

        return data;
    }
}