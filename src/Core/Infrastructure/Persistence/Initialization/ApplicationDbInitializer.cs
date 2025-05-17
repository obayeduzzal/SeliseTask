using TTM.Core.Infrastructure.Persistence.Context;

namespace TTM.Core.Infrastructure.Persistence.Initialization;

internal class ApplicationDbInitializer(
    MainDbContext mainDbContext,
    ApplicationDbSeeder dbSeeder,
    ILogger<ApplicationDbInitializer> logger)
{
    private readonly MainDbContext _mainDbContext = mainDbContext;
    private readonly ApplicationDbSeeder _dbSeeder = dbSeeder;
    private readonly ILogger<ApplicationDbInitializer> _logger = logger;

    public async Task InitializeAsync(CancellationToken ct)
    {
        if (_mainDbContext.Database.GetPendingMigrationsAsync(cancellationToken: ct).GetAwaiter().GetResult().Any())
        {
            _logger.LogInformation("Applying Migrations...");

            await _mainDbContext.Database.MigrateAsync(ct);
        }

        if (await _mainDbContext.Database.CanConnectAsync(ct))
            await _dbSeeder.SeedDataAsync(ct);
    }
}
