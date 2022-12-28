using Redis.Persistence;
using StackExchange.Redis;

namespace RedisWith.NET
{
    public static class RedisIocInstaller
    {
        public static void Install(IServiceCollection services, ConfigurationManager configurationManager)
        {
            var connectionStr = configurationManager.GetConnectionString("DefaultConnection");

            var multiplexer = ConnectionMultiplexer.Connect(connectionStr);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddScoped<IRedisPersistence, RedisPersistence>();
        }
    }
}
