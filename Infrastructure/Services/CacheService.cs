using CrealutionServer.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Threading.Tasks;

namespace CrealutionServer.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetData<T>(string key)
            where T : class
        {
            var valueBytes = await _cache.GetAsync(key);

            if (valueBytes == null)
                return null;

            var valueJson = Encoding.UTF8.GetString(valueBytes);

            if (string.IsNullOrEmpty(valueJson))
                return null;

            return JsonConvert.DeserializeObject<T>(valueJson);
        }

        public async Task SetData<T>(
            string key, 
            T value, 
            double expirationTime)
            where T : class
        {
            var valueJson = JsonConvert.SerializeObject(value);
            var valueBytes = Encoding.UTF8.GetBytes(valueJson);
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(expirationTime));

            await _cache.SetAsync(
                key,
                valueBytes,
                options);
        }

        public async Task RemoveData(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}