using Enyim.Caching;
using Enyim.Caching.Configuration;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Service;

//public class MemcachedService
//{
//    private readonly IMemcachedClient _client;

//    public MemcachedService()
//    {
//        //_client = new MemcachedClient();
//        _client = new MemcachedClient(options => options.AddServer("memcached", 11211));
//    }

//    public async Task SetAsync(string key, TestData data) =>  await _client.SetAsync(key, data);

//    public async Task<TestData?> GetAsync(string key)
//    {
//        var result = await _client.GetAsync<TestData>(key);
//        return result.Success ? result.Value : null;
//    }
//}