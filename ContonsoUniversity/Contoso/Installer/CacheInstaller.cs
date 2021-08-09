using Contoso.Helpers;
using Contoso.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Installer
{
    public static class CacheInstaller 
    {
        public static IServiceCollection AddCacheServices( this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
            {
                return services;
            }

            services.AddStackExchangeRedisCache(options => 
            {
                options.Configuration = redisCacheSettings.ConnectionString;
                options.InstanceName = redisCacheSettings.InstanceName;
            }) ;
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            return services;
        }
    }
}
