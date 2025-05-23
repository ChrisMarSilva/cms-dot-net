﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cache.App.Api.Services;

public interface ICacheService
{
    public Task<T?> GetCacheValueAsync<T>(string key);
    public Task SetCacheValueAsync<T>(string key, T value, TimeSpan? expiry = null);
    public Task RemoveCacheValueAsync(string key);
}

//private static readonly MessagePack.MessagePackSerializerOptions _lz4Options = MessagePack.MessagePackSerializerOptions.Standard.WithCompression((MessagePack.MessagePackCompression)2);
//return MessagePack.MessagePackSerializer.Serialize<IdempotencyRegister>(instance, _lz4Options, default(CancellationToken));
//return MessagePack.MessagePackSerializer.Deserialize<IdempotencyRegister>(bytes, _lz4Options, default(CancellationToken));

public class RedisCacheService : ICacheService
{
    private readonly ILogger<RedisCacheService> _logger;
    private readonly IDatabase _cache;
    //private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly JsonSerializerOptions _options;

    public RedisCacheService(ILogger<RedisCacheService> logger, IDatabase cache)
    {
        _logger = logger;
        _cache = cache;
        //_connectionMultiplexer = connectionMultiplexer;

        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true,
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
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

    public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        //var _cache = _connectionMultiplexer.GetDatabase();

        var json = JsonSerializer.Serialize<T>(value, _options);

        await _cache.StringSetAsync(key, json, expiry: expiry, flags: CommandFlags.FireAndForget);
        //var bytes = Encoding.UTF8.GetBytes(json);
        //return _cache.SetAsync(key, bytes, options);
    }

    public async Task RemoveCacheValueAsync(string key)
    {
        //var _cache = _connectionMultiplexer.GetDatabase();

        bool wasRemoved = await _cache.KeyDeleteAsync(key);

        // if (!wasRemoved) throw new Exception($"Key {key} was not found in the cache.");
    }
}

public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private static MemoryCacheEntryOptions _options;

    public InMemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
        _options = new MemoryCacheEntryOptions()
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

    public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        _cache.Set(key, value, _options);
    }

    public async Task RemoveCacheValueAsync(string key)
    {
        _cache.Remove(key);
    }
}

public class HybridCacheService
{
    private readonly ILogger<HybridCacheService> _logger;
    private readonly HybridCache _cache;
    //private readonly InMemoryCacheService _cacheInMemory;
    //private readonly RedisCacheService _cacheRedis;
    private static HybridCacheEntryOptions _options;

    public HybridCacheService(ILogger<HybridCacheService> logger, HybridCache cache)
    {
        _logger = logger;
        _cache = cache;
        //_cacheInMemory = cacheInMemory;
        //_cacheRedis = cacheRedis;

        var _options = new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(10),
            LocalCacheExpiration = TimeSpan.FromMinutes(2),
            Flags = HybridCacheEntryFlags.DisableDistributedCache
        };
    }

    public async Task<T?> GetCacheValueAsync<T>(string key)
    {
        //var value = default(T);

        //value = await _cacheInMemory.GetCacheValueAsync<T>(key);
        //if (value is not null)
        //    return value;

        //value = await _cacheRedis.GetCacheValueAsync<T>(key);
        //if (value is null)
        //    return default(T);

        var value = await _cache.GetOrCreateAsync<T>(key, async token => default(T), _options);

        return value;
    }

    public async Task SetCacheValueAsync<T>(string key, T value)
    {
        //await _cacheInMemory.SetCacheValueAsync<T>(key, value);
        //await _cacheRedis.SetCacheValueAsync<T>(key, value);
        await _cache.SetAsync<T>(key, value, _options);
    }

    public async Task RemoveCacheValueAsync(string key)
    {
        //await _cacheInMemory.RemoveCacheValueAsync(key);
        //await _cacheRedis.RemoveCacheValueAsync(key);
        await _cache.RemoveAsync(key);
    }
}