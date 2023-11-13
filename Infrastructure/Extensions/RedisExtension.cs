using Core.Redis;
using Infrastructure.Redis.Base;
using Infrastructure.Settings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Infrastructure.Extensions;

internal static class RedisExtension
{
    internal static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection redisSettingsConfig = configuration.GetSection("Cache");

        RedisCacheConfigurations redisSettings = redisSettingsConfig.Get<RedisCacheConfigurations>();

        services.Configure<RedisCacheConfigurations>(redisSettingsConfig);
        
        services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(_ =>
        {
            return new[] { redisSettings.Redis };
        });

        services.AddSingleton<ITestCacheService, TestRedisCacheService>();

        return services;
    }
}