using Npgsql;

namespace AutonomyApi
{
    public static class AppSettings
    {
        static IConfigurationRoot _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        static internal readonly string JwtIssuer = _configuration["Jwt:Issuer"] ?? string.Empty;
        static internal readonly string JwtKey = _configuration["Jwt:Key"] ?? string.Empty;
        static internal readonly string ConnectionString = _configuration.GetConnectionString("Postgres") ?? string.Empty;

    }
}
