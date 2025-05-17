namespace TTM.Core.Infrastructure.Persistence.ConnectionString;

public interface IConnectionStringValidator
{
    bool TryValidate(string connectionString);
}