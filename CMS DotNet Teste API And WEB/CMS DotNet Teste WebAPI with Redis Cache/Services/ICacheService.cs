﻿namespace WebApplication1.Services
{
    public interface ICacheService
    {
        public Task<string> GetCacheValueAsync(string key);
        public Task SetCacheValueAsync(string key, string value);
    }
}
