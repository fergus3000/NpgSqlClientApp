using Microsoft.Extensions.Logging;
using Npgsql;

namespace DbClientApp
{
    public interface IConnectionFactory
    {
        NpgsqlConnection GetConnection(string appname);
    }


    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IDbConnectionStringProvider _dbConnectionStringProvider;
        readonly ILogger<ConnectionFactory> _logger;

        public ConnectionFactory(IDbConnectionStringProvider dbConnectionStringProvider, ILogger<ConnectionFactory> logger)
        {
            Npgsql.NpgsqlConnection.GlobalTypeMapper.UseNodaTime();
            _dbConnectionStringProvider = dbConnectionStringProvider;
            _logger = logger;
        }

        public NpgsqlConnection GetConnection(string appname) => new NpgsqlConnection(_dbConnectionStringProvider.GetConnectionStringForApp(appname));
    }

    

}
