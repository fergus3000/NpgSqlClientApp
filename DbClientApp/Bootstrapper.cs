using Microsoft.Extensions.DependencyInjection;

namespace DbClientApp
{
    internal class Bootstrapper
    {
        public static void Bootstrap(IServiceCollection services)
        {
            services.AddSingleton<DbClientAppMain>();
            services.AddSingleton<IDbConnectionConfig, DbConfigFromEnvironmentVars>();
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        }
    }
}
