using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
//using NLog;

namespace DbClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var myService = services.GetRequiredService<DbClientAppMain>();
                    await myService.Run(args, default);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The application thew an exception: " + ex);
                    throw;
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddNLog();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    Bootstrapper.Bootstrap(services);
                })
                .UseConsoleLifetime();
            return host;
        }
    }

}
