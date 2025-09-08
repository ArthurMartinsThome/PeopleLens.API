namespace PL.Infra.Util.Model
{
    public class EnvironmentSettings
    {
        public string ConnectionString { get; init; } = string.Empty;
        public string JWT_Key { get; init; } = string.Empty;
        public string JWT_Issuer { get; init; } = string.Empty;
        public string JWT_Audience { get; init; } = string.Empty;
    }
}