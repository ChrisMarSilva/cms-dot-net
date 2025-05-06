using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Caching.Memory;
using TesteBenchmarkDotNet.Models;
using TesteBenchmarkDotNet.Service;

namespace TesteBenchmarkDotNet.Benchmarks;

[MemoryDiagnoser]
public class BenchmarkCache
{
    private TestData _data;

    private RedisService _redis;
    private PostgresService _pg;
    private MemoryCacheService _memory;
    private LiteDbCacheService _liteDbCache;
    private NCacheService _ncache;
    private DictionaryService _dictCache;
    //private MemoryCacheService _sqlServerCache;

    [GlobalSetup]
    public void Setup()
    {
        _data = new TestData(1, "test", $"value {DateTime.Now}");
        
        _redis = new RedisService("localhost:6379,password=123");
        _redis.SetAsync(_data.Name, _data).Wait();

        _pg = new PostgresService("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;");
        _pg.InsertAsync(_data).Wait();

        _memory = new MemoryCacheService(new MemoryCache(new MemoryCacheOptions()));
        _memory.Set(_data.Name, _data);

        _liteDbCache = new LiteDbCacheService();
        _liteDbCache.Set(_data.Name, _data);

        _ncache = new NCacheService();
        _ncache.Set(_data.Name, _data);

        _dictCache = new DictionaryService();
        _dictCache.Set(_data.Name, _data);
    }

    [Benchmark] public async Task<TestData?> GetFromRedis() => await _redis.GetAsync(_data.Name);
    [Benchmark] public async Task<TestData?> GetFromPostgres() => await _pg.GetAsync(_data.Name);
    [Benchmark] public TestData? GetFromMemoryCache() => _memory.Get(_data.Name);
    [Benchmark] public TestData? GetFromLiteDb() => _liteDbCache.Get(_data.Id.ToString());
    [Benchmark] public TestData? GetFromNCache() => _ncache.Get(_data.Id.ToString());
    [Benchmark] public TestData? GetFromConcurrentDict() => _dictCache.Get(_data.Id.ToString());
    //[Benchmark] public async Task<TestData?> GetFromSqlServerCache() => await _sqlServerCache.GetAsync(_data.Name);
}


/*

| Method          | Mean       | Error    | StdDev   | Gen0   | Allocated |
|---------------- |-----------:|---------:|---------:|-------:|----------:|
| GetFromRedis    |   704.4 us | 11.43 us |  9.54 us |      - |   1.02 KB |
| GetFromPostgres | 1,123.4 us | 22.23 us | 45.91 us | 1.9531 |   3.09 KB |

| Method                | Mean            | Error         | StdDev         | Median          | Gen0    | Allocated |
|---------------------- |----------------:|--------------:|---------------:|----------------:|--------:|----------:|
| GetFromRedis          | 1,133,220.07 ns | 74,300.415 ns | 219,076.466 ns | 1,213,221.00 ns |       - |    1044 B |
| GetFromPostgres       | 1,097,748.91 ns | 21,847.575 ns |  53,180.020 ns | 1,078,338.57 ns |  1.9531 |    3158 B |
| GetFromMemoryCache    |        64.21 ns |      0.368 ns |       0.288 ns |        64.21 ns |       - |         - |
| GetFromLiteDb         |    26,056.89 ns |    351.600 ns |     311.684 ns |    26,100.91 ns | 10.2539 |   16239 B |
| GetFromNCache         |       609.42 ns |      7.637 ns |       6.770 ns |       609.74 ns |  0.4377 |     688 B |
| GetFromConcurrentDict |        60.88 ns |      0.320 ns |       0.299 ns |        60.77 ns |       - |         - |


 */