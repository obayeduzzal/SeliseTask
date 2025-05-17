using TTM.Core.Infrastructure.Persistence.Initialization;

namespace TTM.Core.Infrastructure.Persistence;

internal class DatabaseInitializer(IServiceProvider serviceProvider) : IDatabaseInitializer
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
            .InitializeAsync(cancellationToken);
    }
}
