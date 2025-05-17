using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TTM.Core.Modules;

public static class Startup
{
    public static IServiceCollection AddModules(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return services
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    }
}