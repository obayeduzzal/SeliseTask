using Microsoft.Extensions.Options;
using Npgsql;

namespace TTM.Core.Infrastructure.Persistence.ConnectionString;

public class ConnectionStringSecurer(IOptions<DatabaseSettings> dbSettings) : IConnectionStringSecurer
{
    private const string HiddenValueDefault = "*******";
    private readonly DatabaseSettings _dbSettings = dbSettings.Value;

    public string? MakeSecure(string? connectionString)
    {
        if (connectionString == null || string.IsNullOrEmpty(connectionString))
            return connectionString;

        return MakeSecureNpgsqlConnectionString(connectionString);
    }

    private static string MakeSecureNpgsqlConnectionString(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);

        if (!string.IsNullOrEmpty(builder.Password))
            builder.Password = HiddenValueDefault;

        if (!string.IsNullOrEmpty(builder.Username))
            builder.Username = HiddenValueDefault;

        return builder.ToString();
    }
}