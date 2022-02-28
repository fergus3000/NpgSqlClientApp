using Microsoft.Extensions.Logging;

namespace DbClientApp
{
    public class DbClientAppMain
    {
        private readonly ILogger<DbClientAppMain> _logger;
        private readonly IConnectionFactory _connectionFactory;

        public DbClientAppMain(IConnectionFactory connectionFactory, ILogger<DbClientAppMain> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }


        public async Task Run(string[] args, CancellationToken ct)
        {
            _logger.LogInformation("xxxxx");
        }
    }
}
