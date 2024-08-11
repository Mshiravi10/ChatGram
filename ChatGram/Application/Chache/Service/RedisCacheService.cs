using Application.Chache.IService;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;
namespace Application.Chache.Service
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var jsonValue = await _db.StringGetAsync(key);
            if (jsonValue.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonValue);
        }

        public Task<bool> KeyExistsAsync(string key)
        {
            return _db.KeyExistsAsync(key);
        }

        public Task<bool> RemoveAsync(string key)
        {
            return _db.KeyDeleteAsync(key);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var jsonValue = JsonSerializer.Serialize(value);
            return _db.StringSetAsync(key, jsonValue, expiry);
        }
    }
}
