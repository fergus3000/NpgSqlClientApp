using Microsoft.Extensions.Configuration;

namespace DbClientApp
{
    public interface IAppConfiguration
    {
        string KeyvaultBaseUrl { get; }
        string AKSClusterUrl { get; }
    }

    public class AppConfiguration : IAppConfiguration
    {
        private readonly IConfiguration _config;

        public AppConfiguration(IConfiguration config)
        {
            _config = config;
        }
        public string KeyvaultBaseUrl => _config.GetValue<string>("KeyvaultBaseUrl") ?? "NOTSET";
        public string AKSClusterUrl => _config.GetValue<string>("AKSClusterUrl") ?? "NOTSET";
        
    }
}
