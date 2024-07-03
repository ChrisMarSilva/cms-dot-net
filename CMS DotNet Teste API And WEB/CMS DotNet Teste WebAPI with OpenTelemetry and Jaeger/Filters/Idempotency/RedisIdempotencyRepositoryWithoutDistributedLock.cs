using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Project.Filters.Idempotency;

internal class RedisIdempotencyRepositoryWithoutDistributedLock : RedisIdempotencyRepository
{
    public RedisIdempotencyRepositoryWithoutDistributedLock(IDatabase database, IIdempotencySerializer serializer, IOptions<IdempotencyOptions> options, IServiceProvider serviceProvider) : base(database, serializer, options, serviceProvider)
    {
    }

    public override async Task<bool> TryAddAsync(string key)
    {
        using (CreateScopeForRedis("Exists", "TryAddAsync", $"EXISTS \'{key}\'"))
        {
            if (await _database.KeyExistsAsync(key))
                return false;
        }

        var value = _serializer.Serialize(IdempotencyRegister.Of(key), IdempotencyRegisterCustomContext.Default.IdempotencyRegister);

        using (CreateScopeForRedis("Set", "TryAddAsync", $"SET \'{key}\' \'{value}\'"))
        {
            return await _database.StringSetAsync(key, value, TimeSpan.FromMinutes(1), when: When.NotExists);
        }
    }
}