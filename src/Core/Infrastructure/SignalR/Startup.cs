using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Serilog;
using ILogger = Serilog.ILogger;

namespace TTM.Core.Infrastructure.SignalR;

internal static class Startup
{
    internal static IServiceCollection AddSignalRWebsocket(this IServiceCollection services, IConfiguration config)
    {
        ILogger logger = Log.ForContext(typeof(Startup));

        var signalRSettings = config.GetSection(nameof(SignalRSettings)).Get<SignalRSettings>();
        if (signalRSettings is null)
            ErrorHelper.ThrowBadRequestException("signalRSettings", "SignalR settings not configure.");

        if (!signalRSettings!.UseBackplane)
        {
            services.AddSignalR();
        }
        else
        {
            var backplaneSettings = config.GetSection("SignalRSettings:Backplane").Get<SignalRSettings.Backplane>();
            if (backplaneSettings is null)
                ErrorHelper.ThrowBadRequestException("Redis Backplane", "Backplane enabled, but no backplane settings in config.");

            switch (backplaneSettings!.Provider)
            {
                case "redis":
                    if (backplaneSettings.Connection is null)
                        ErrorHelper.ThrowBadRequestException("Redis backplane connectionString", "Redis backplane provider: No connectionString configured.");

                    services.AddSignalR().AddStackExchangeRedis(backplaneSettings.Connection!, options =>
                    {
                        options.Configuration.AbortOnConnectFail = false;
                    });

                    break;

                default:
                    ErrorHelper.ThrowBadRequestException("Invalid Provider", $"SignalR backplane Provider {backplaneSettings.Provider} is not supported.");
                    break;
            }

            logger.Information("SignalR Backplane Current Provider: {Provider}.", backplaneSettings.Provider);
        }

        return services;
    }

    internal static IEndpointRouteBuilder MapSignalRHub(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<MessagingHub>("/messaging-hub", options =>
        {
            options.CloseOnAuthenticationExpiration = true;
            options.TransportMaxBufferSize = 999999;
            options.ApplicationMaxBufferSize = 999999;
        });

        return endpoints;
    }
}