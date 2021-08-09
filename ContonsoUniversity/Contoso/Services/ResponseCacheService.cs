using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey,Object response,TimeSpan absoluteExpiratyTime, TimeSpan unsuedExpiraryTime)
        {
            if(response == null)
            {
                return;
            }
            var serialisedResponse = JsonConvert.SerializeObject(response);
            await _distributedCache.SetStringAsync(cacheKey, serialisedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiratyTime,
                SlidingExpiration = unsuedExpiraryTime
            });
        }

        public async Task<string> GetCachedResponseAsync (string cacheKey)
        {
            var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
            if(cacheResponse is null)
            {
                return null;
            }
            return cacheResponse;
        }
    }
}
