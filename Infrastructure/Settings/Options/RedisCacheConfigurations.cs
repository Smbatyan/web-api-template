using StackExchange.Redis.Extensions.Core.Configuration;

namespace Infrastructure.Settings.Options;

public class RedisCacheConfigurations
{
    public RedisConfiguration Redis { get; set; }
    public List<RedisServiceConfigurations> Databases { get; set; }
}

public class RedisServiceConfigurations
{
    public string ServiceName { get; set; }
    public int Database { get; set; }
    public int? ExpireSeconds { get; set; }
    public bool Enabled { get; set; }
}