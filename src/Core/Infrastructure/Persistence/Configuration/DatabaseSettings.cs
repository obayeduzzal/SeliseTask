namespace TTM.Core.Infrastructure.Persistence;

public class DatabaseSettings
{
    public required string DbProvider { get; set; }
    public required string ConnectionString { get; set; }
    public int? CommandTimeout { get; set; }
}