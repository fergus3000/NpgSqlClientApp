using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DbClientApp
{
    internal class Bootstrapper
    {
        public static void Bootstrap(IServiceCollection services)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration config = configBuilder.Build();

            services.AddSingleton(config);
            services.AddSingleton<DbClientAppMain>();
            services.AddSingleton<IDbConnectionStringProvider, EnvConnStringProvider>();
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddSingleton<IAppConfiguration, AppConfiguration>();
        }
    }
}
