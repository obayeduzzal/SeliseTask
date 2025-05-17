using TTM.Core.Infrastructure.Persistence.ConnectionString;
using TTM.Core.Infrastructure.Persistence.Context;
using TTM.Core.Infrastructure.Persistence.Initialization;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Dynamic;
using System;
using System.Linq;

namespace TTM.Core.Infrastructure.Persistence;

public static class Startup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var databaseSettings = config.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        if (databaseSettings == null)
            ErrorHelper.ThrowBadRequestException("DatabaseSettings", "DatabaseSettings is not configured.");

        string dProvider = databaseSettings!.DbProvider;
        if (string.IsNullOrEmpty(dProvider))
            throw new InvalidOperationException($"DB Provider {dProvider} is not configured.");

        string dbConnectionString = databaseSettings!.ConnectionString;
        if (string.IsNullOrEmpty(dbConnectionString))
            throw new InvalidOperationException($"DB ConnectionString is not configured.");

        services.Configure<DatabaseSettings>(config.GetSection(nameof(DatabaseSettings)));
        services.AddDbContext<MainDbContext>(m => m.UseDatabase(databaseSettings));
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddTransient<ApplicationDbInitializer>();
        services.AddTransient<ApplicationDbSeeder>();
        services.AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>();
        services.AddTransient<IConnectionStringValidator, ConnectionStringValidator>();
        services.AddRepositories();
        services.AddScoped(typeof(IRepository<>), typeof(MainDbRepository<>));

        return services;
    }

    public static DbContextOptionsBuilder UseDatabase(
        this DbContextOptionsBuilder builder,
        DatabaseSettings databaseSettings)
    {
        string dbProvider = databaseSettings.DbProvider.ToUpperInvariant();

        if (dbProvider == DbProviders.PostgreSQL)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            return builder.UseNpgsql(databaseSettings.ConnectionString, e =>
            {
                e.MigrationsAssembly("TTM.Migrators.PostgreSQL");
                if (databaseSettings.CommandTimeout.HasValue)
                    e.CommandTimeout(databaseSettings.CommandTimeout);
            });
        }

        throw new InvalidOperationException($"DB Provider {databaseSettings.DbProvider} is not supported.");
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Add Repositories

        foreach (var aggregateRootType in
            typeof(IAggregateRoot).Assembly.GetExportedTypes()
                .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                .ToList())
        {
            // Add ReadRepositories.
            services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));
        }

        return services;
    }
}