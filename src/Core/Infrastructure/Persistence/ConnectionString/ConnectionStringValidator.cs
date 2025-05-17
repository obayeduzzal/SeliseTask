using Microsoft.Extensions.Logging;
using Npgsql;

namespace TTM.Core.Infrastructure.Persistence.ConnectionString;

internal class ConnectionStringValidator(ILogger<ConnectionStringValidator> logger) : IConnectionStringValidator
{
    private readonly ILogger<ConnectionStringValidator> _logger = logger;

    public bool TryValidate(string connectionString)
    {
        try
        {
            _ = new NpgsqlConnectionStringBuilder(connectionString);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Connection String Validation Exception : {ex.Message}");

            return false;
        }
    }
}