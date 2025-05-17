namespace TTM.Core.Infrastructure.Persistence.ConnectionString;

public interface IConnectionStringSecurer
{
    string? MakeSecure(string? connectionString);
}