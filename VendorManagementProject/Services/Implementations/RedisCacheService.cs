using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VendorManagementProject.Services.Interface;

namespace VendorManagementProject.Services.Class
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCacheAsync<T>(string key, T data, TimeSpan timeToLive)
        {
            var serializedData = JsonSerializer.Serialize(data);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            };
            await _cache.SetStringAsync(key, serializedData, options);
        }

        public async Task<T> GetFromCacheAsync<T>(string key)
        {
            var cachedData = await _cache.GetStringAsync(key);
            if (cachedData == null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task RemoveFromCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}