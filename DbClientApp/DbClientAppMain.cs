﻿using Microsoft.Extensions.Logging;

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
            await DoSomeQuery(ct);
        }

        public async Task DoSomeQuery(CancellationToken ct)
        {
            try
            {
                await using var dbConn = _connectionFactory.GetConnection();
                await dbConn.OpenAsync(ct);
                await using var dbCmd = dbConn.CreateCommand();
                _logger.LogInformation("Starting some query");
                dbCmd.CommandText = "SELECT COUNT(*) FROM pg_settings";
                var someResult = Convert.ToInt32(await dbCmd.ExecuteScalarAsync(ct));
                _logger.LogInformation($"Got result..{someResult}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error running some query");
                throw;
            }
        }
    }
}
