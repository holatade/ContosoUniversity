using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, Object response, TimeSpan absoluteExpiratyTime, TimeSpan unsuedExpiraryTime);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
