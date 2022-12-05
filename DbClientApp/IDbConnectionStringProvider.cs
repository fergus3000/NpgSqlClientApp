namespace DbClientApp
{
    public interface IDbConnectionStringProvider
    {
        string GetConnectionStringForApp(string tenantGuid);
    }
}