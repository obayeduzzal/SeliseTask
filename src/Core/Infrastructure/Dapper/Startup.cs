using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TTM.Core.Infrastructure.Dapper;

internal static class Startup
{
    internal static IServiceCollection AddDapper(this IServiceCollection services, IConfiguration config) =>
        services.AddSingleton<DapperContext>();
}