using Microsoft.Extensions.Options;
using RedLockNet.SERedis.Configuration;
using RedLockNet.SERedis;
using StackExchange.Redis;
using System.Text.Json.Serialization.Metadata;

namespace Project.Filters.Idempotency;

internal class RedisIdempotencyRepository : IIdempotencyRepository
{
    protected readonly IDatabase _database;
    protected readonly IIdempotencySerializer _serializer;
    protected readonly IOptions<IdempotencyOptions> _options;
    protected readonly IServiceProvider _serviceProvider;

    public RedisIdempotencyRepository(IDatabase database, IIdempotencySerializer serializer, IOptions<IdempotencyOptions> options, IServiceProvider serviceProvider)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _serviceProvider = serviceProvider ?? throw new ArgumentException(nameof(serviceProvider));
    }

    protected static IDisposable CreateScopeForRedis(string operationName, string method, string dbStatement)
    {
        return IdempotencyTelemetry.ActivitySource.StartActivity($"Redis {operationName}")?
            .AddTag("db.type", "redis")
            .AddTag("db.method", method)
            .AddTag("db.statement", dbStatement);
    }

    public virtual async Task<T> GetAsync<T>(string key) where T : IIdempotencyRegister
    {
        using var scope = CreateScopeForRedis("Get", "GetAsync", $"GET \'{key}\'");

        var value = await _database.StringGetAsync(key);
        return _serializer.Deserialize(value, IdempotencyRegisterCustomContext.Default.IdempotencyRegister as JsonTypeInfo<T>);
    }

    public virtual async Task RemoveAsync(string key)
    {
        using var scope = CreateScopeForRedis("Delete", "RemoveAsync", $"DELETE \'{key}\'");

        await _database.KeyDeleteAsync(key);
    }

    public virtual async Task<bool> TryAddAsync(string key)
    {
        using (CreateScopeForRedis("Exists", "TryAddAsync", $"EXISTS \'{key}\'"))
        {
            if (await _database.KeyExistsAsync(key))
                return false;
        }

        using (CreateScopeForRedis("Lock", "TryAddAsync", $"LOCK \'{key}\'"))
        {
            using var factory = RedLockFactory.Create(new[] { new RedLockMultiplexer(_database.Multiplexer) });
            await using var redLock = await factory.CreateLockAsync(key, TimeSpan.FromMinutes(1));

            if (!redLock.IsAcquired || await _database.KeyExistsAsync(key))
                return false;
        }

        var value = _serializer.Serialize(IdempotencyRegister.Of(key), IdempotencyRegisterCustomContext.Default.IdempotencyRegister);

        using (CreateScopeForRedis("Set", "TryAddAsync", $"SET \'{key}\' \'{value}\'"))
        {
            return await _database.StringSetAsync(key, value, TimeSpan.FromMinutes(1), when: When.NotExists);
        }
    }

    public virtual async Task UpdateAsync<T>(string key, T register) where T : IIdempotencyRegister
    {
        var value = _serializer.Serialize(register, IdempotencyRegisterCustomContext.Default.IdempotencyRegister as JsonTypeInfo<T>);

        using var scope = CreateScopeForRedis("Update", "UpdateAsync", $"UPDATE \'{key}\' \'{value}\'");

        await _database.StringSetAsync(key, value, TimeSpan.FromHours(_options.Value.TTLInHours), when: When.Exists);
    }
}