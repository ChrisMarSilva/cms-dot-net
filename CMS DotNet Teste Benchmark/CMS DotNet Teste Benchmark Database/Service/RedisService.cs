using StackExchange.Redis;
using System.Text.Json;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Service;

public class RedisService
{
    private readonly IDatabase _db;

    public RedisService(string connection)
    {
        var redis = ConnectionMultiplexer.Connect(connection);
        _db = redis.GetDatabase();
    }

    public async Task SetAsync(string key, TestData data) =>
        await _db.StringSetAsync(key, JsonSerializer.Serialize(data));

    public async Task<TestData?> GetAsync(string key)
    {
        var value = await _db.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<TestData>(value!) : null;
    }
}