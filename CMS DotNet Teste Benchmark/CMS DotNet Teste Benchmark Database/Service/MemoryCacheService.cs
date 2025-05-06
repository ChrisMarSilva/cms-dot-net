using Microsoft.Extensions.Caching.Memory;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Service;

public class MemoryCacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache) => _cache = cache;

    public void Set(string key, TestData data) =>  _cache.Set(key, data);

    public TestData? Get(string key) => _cache.TryGetValue(key, out TestData? data) ? data : null;
}