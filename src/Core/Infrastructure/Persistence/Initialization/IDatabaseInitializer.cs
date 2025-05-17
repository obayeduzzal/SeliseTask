namespace TTM.Core.Infrastructure.Persistence;
internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
}
