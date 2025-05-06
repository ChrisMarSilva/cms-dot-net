using System.Collections.Concurrent;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Service;

public class DictionaryService
{
    private readonly ConcurrentDictionary<string, (TestData value, DateTime expiration)> _cache = new();
    private readonly TimeSpan _ttl = TimeSpan.FromMinutes(5);

    public DictionaryService()
    {
       
    }

    public void Set(string key, TestData data) =>
         _cache[key] = (data, DateTime.UtcNow.Add(_ttl));

    public TestData? Get(string key)
    {
        if (_cache.TryGetValue(key, out var entry) && DateTime.UtcNow < entry.expiration)
            return entry.value;

        _cache.TryRemove(key, out _);
        return null;
    }
}
