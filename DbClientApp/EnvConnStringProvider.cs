using Microsoft.Extensions.Logging;
using Npgsql;

namespace DbClientApp
{
    public class EnvConnStringProvider : IDbConnectionStringProvider
    {
        private readonly ILogger<EnvConnStringProvider> _logger;

        public EnvConnStringProvider(ILogger<EnvConnStringProvider> logger)
        {
            _logger = logger;
        }

        public string GetConnectionStringForApp(string tenantGuid)
        {
            _logger.LogInformation("Getting connstring from environment vars:" + $"User:{UserName}|Host:{Host}|Port:{Port}|Database:{Database}");

            var connStrBuilder = new NpgsqlConnectionStringBuilder();
            connStrBuilder.Port = Port;
            connStrBuilder.Host = Host;
            connStrBuilder.Username = UserName;
            connStrBuilder.Password = Password;
            connStrBuilder.Database = Database;
            connStrBuilder.MaxPoolSize = 10;
            return connStrBuilder.ToString();
        }

        public string UserName => Environment.GetEnvironmentVariable("DB_USER") ?? "NOT SET";

        public string Password => Environment.GetEnvironmentVariable("DB_PASS") ?? "NOT SET";

        public string Host => Environment.GetEnvironmentVariable("DB_HOST") ?? "NOT SET";

        public int Port => Convert.ToInt32(Environment.GetEnvironmentVariable("DB_PORT"));

        public string Database => Environment.GetEnvironmentVariable("DB_NAME") ?? "NOT SET";
    }
}
