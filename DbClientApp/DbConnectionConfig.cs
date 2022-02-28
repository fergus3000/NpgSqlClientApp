namespace DbClientApp
{
    public interface IDbConnectionConfig
    {
        public string UserName { get; }
        public string Password { get; }
        public string Host { get; }
        public int Port { get; }
        public string Database { get; }
    }
    public class DbConfigFromEnvironmentVars : IDbConnectionConfig
    {
        public string UserName => Environment.GetEnvironmentVariable("DB_USER");

        public string Password => Environment.GetEnvironmentVariable("DB_PASS");

        public string Host => Environment.GetEnvironmentVariable("DB_HOST");

        public int Port => Convert.ToInt32(Environment.GetEnvironmentVariable("DB_PORT"));

        public string Database => Environment.GetEnvironmentVariable("DB_NAME");

        public override string ToString()
        {
            return $"User:{UserName}|Host:{Host}|Port:{Port}|Database:{Database}";
        }
    }
}
