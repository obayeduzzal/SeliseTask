using System.Data;
using TTM.Core.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TTM.Core.Infrastructure.Dapper;

public class DapperContext
{
    private readonly string _connectionString;
    private readonly string _dbProvider;

    public DapperContext(IConfiguration configuration)
    {
        var dbSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>()
            ?? throw new InvalidOperationException("Database settings are not configured!");

        _connectionString = dbSettings.ConnectionString
            ?? throw new InvalidOperationException("DB ConnectionString is not configured!");

        _dbProvider = dbSettings.DbProvider
            ?? throw new InvalidOperationException("DB Provider is not configured!");
    }

    public IDbConnection CreateConnection() => _dbProvider switch
    {
        DbProviders.PostgreSQL => new NpgsqlConnection(_connectionString),
        _ => throw new InvalidOperationException($"DB Provider {_dbProvider} is not supported!")
    };
}
