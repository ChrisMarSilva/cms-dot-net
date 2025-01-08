using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System.Text.Json;

namespace Cache.Api.Services;

public interface ICacheService
{
    public Task<T?> GetCacheValueAsync<T>(string key);
    public Task SetCacheValueAsync<T>(string key, T value);
    public Task RemoveCacheValueAsync(string key);
}

public class RedisCacheService : ICacheService
{
    private readonly ILogger<RedisCacheService> _logger;
    private readonly IDatabase _cache;
    //private readonly IConnectionMultiplexer _connectionMultiplexer;
    private static JsonSerializerOptions _options;

    public RedisCacheService(ILogger<RedisCacheService> logger, IDatabase cache)
    {
        _logger = logger;
        _cache = cache;
        //_connectionMultiplexer = connectionMultiplexer;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };
    }

    public async Task<T?> GetCacheValueAsync<T>(string key)
    {
        //var _cache = _connectionMultiplexer.GetDatabase();

        string? json = await _cache.StringGetAsync(key);
        if (string.IsNullOrEmpty(json))
            return default(T);

        var data = JsonSerializer.Deserialize<T>(json, _options) ?? default(T);
        return data;
    }

    public async Task SetCacheValueAsync<T>(string key, T value)
    {
        //var _cache = _connectionMultiplexer.GetDatabase();

        var json = JsonSerializer.Serialize<T>(value, _options);

        await _cache.StringSetAsync(key, json, expiry: TimeSpan.FromSeconds(10), flags: CommandFlags.FireAndForget);
    }

    public async Task RemoveCacheValueAsync(string key)
    {
        //var _cache = _connectionMultiplexer.GetDatabase();

        bool wasRemoved = await _cache.KeyDeleteAsync(key);

        if (!wasRemoved) throw new Exception($"Key {key} was not found in the cache.");
    }
}

public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private static MemoryCacheEntryOptions _options;

    public InMemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
        var _options = new MemoryCacheEntryOptions()
           .SetSlidingExpiration(TimeSpan.FromSeconds(60))
           .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)) // Expiração absoluta opcional
           .SetPriority(CacheItemPriority.High); // Define prioridade do cache
    }

    public async Task<T?> GetCacheValueAsync<T>(string key)
    {
        if (_cache.TryGetValue(key, out T value))
            return value ?? default(T);

        return default(T);
    }

    public async Task SetCacheValueAsync<T>(string key, T value)
    {
        _cache.Set(key, value, _options);
    }

    public async Task RemoveCacheValueAsync(string key)
    {
        _cache.Remove(key);
    }
}
