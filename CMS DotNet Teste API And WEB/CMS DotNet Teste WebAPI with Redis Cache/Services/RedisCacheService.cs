﻿using StackExchange.Redis;

namespace WebApplication1.Services
{
    public class RedisCacheService: ICacheService
    {

        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            // await db.StringSetAsync(key, value);
            await db.StringSetAsync(key, value, flags: CommandFlags.FireAndForget);
        }
    }
}
