using Microsoft.Extensions.Caching.Distributed;

namespace VendorManagementProject.Services.Interface
{
    public interface IRedisCacheService
    {
        Task SetCacheAsync<T>(string key, T data, TimeSpan timeToLive);
        Task<T> GetFromCacheAsync<T>(string key);
        Task RemoveFromCacheAsync(string key);
    }
}
