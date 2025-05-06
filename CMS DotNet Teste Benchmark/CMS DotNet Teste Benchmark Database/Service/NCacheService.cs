using Alachisoft.NCache.Client;
using System.Text.Json;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Service;

public class NCacheService
{
    private readonly ICache _cache;

    public NCacheService()
    {
        _cache = CacheManager.GetCache(
           "mycache",
           new CacheConnectionOptions
           {
               ServerList = [new ServerInfo("localhost")]
           });
    }

    public void Set(string key, TestData data) =>
        _cache.Insert(key, JsonSerializer.Serialize(data));

    public TestData? Get(string key)
    {
        var json = _cache.Get<string>(key);
        return json is not null ? JsonSerializer.Deserialize<TestData>(json) : null;
        // return _cache.Get<TestData>(key);
    }
}
