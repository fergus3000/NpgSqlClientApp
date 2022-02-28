using Microsoft.Extensions.Logging;
using Npgsql;

namespace DbClientApp
{
    public interface IConnectionFactory
    {
        void Initialize();
        NpgsqlConnection GetConnection();
    }


    public class ConnectionFactory : IConnectionFactory
    {
        readonly IDbConnectionConfig _config;
        readonly ILogger<ConnectionFactory> _logger;
        string _connectionString;
        public ConnectionFactory(IDbConnectionConfig config, ILogger<ConnectionFactory> logger)
        {
            _config = config;
            _logger = logger;
            // why not just Initialize(); here... its a singleton??
            Initialize();
        }

        public void Initialize()
        {
            //NpgsqlConnection.GlobalTypeMapper.UseNodaTime();
            var connStrBuilder = new NpgsqlConnectionStringBuilder();
            connStrBuilder.Port = _config.Port;
            connStrBuilder.Host = _config.Host;
            connStrBuilder.Username = _config.UserName;
            connStrBuilder.Password = _config.Password;
            connStrBuilder.Database = _config.Database;
            connStrBuilder.MaxPoolSize = 10;
            var pwLog = string.IsNullOrEmpty(connStrBuilder.Password) ? "NOTSET" : "****";
            _logger.LogInformation("Initializing Db Conn - Host:{Host} Port:{Port} Db:{Database} User:{User} Pass:{PasswordSet}", connStrBuilder.Host, connStrBuilder.Port, connStrBuilder.Database, connStrBuilder.Username, pwLog);
            _connectionString = connStrBuilder.ToString();
        }

        public NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    }

}
