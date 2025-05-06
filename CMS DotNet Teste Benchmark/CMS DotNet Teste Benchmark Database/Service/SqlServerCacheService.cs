using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.Options;
using System.Text.Json;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Service;

public class SqlServerCacheService
{
    private readonly IDistributedCache _cache;

    public SqlServerCacheService()
    {
        var options = new SqlServerCacheOptions
        {
            ConnectionString = "Server=localhost;Database=CacheDb;User Id=sa;Password=your_password;",
            SchemaName = "dbo",
            TableName = "CacheData"
        };

        /*
         CREATE TABLE dbo.CacheData
(
    Id NVARCHAR(449) NOT NULL PRIMARY KEY,
    Value VARBINARY(MAX) NOT NULL,
    ExpiresAtTime DATETIMEOFFSET NOT NULL,
    SlidingExpirationInSeconds BIGINT NULL,
    AbsoluteExpiration DATETIMEOFFSET NULL
);


         */

        _cache = new SqlServerCache(Options.Create(options));
    }

    public async Task SetAsync(string key, TestData data)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(data);
        await _cache.SetAsync(key, bytes);
    }

    public async Task<TestData?> GetAsync(string key)
    {
        var bytes = await _cache.GetAsync(key);
        return bytes is not null ? JsonSerializer.Deserialize<TestData>(bytes) : null;
    }
}
