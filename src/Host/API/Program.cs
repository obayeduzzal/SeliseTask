using FluentValidation.AspNetCore;
using TTM.Core.Modules;
using TTM.Host;
using Serilog;
using System.Text.Json;
using TTM.Core.Infrastructure.Logger;
using System.Text.Json.Serialization;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

StaticLogger.EnsureInitialized();

Log.Information("Server Booting Up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddConfigurations();

    builder.Host.UseSerilog((_, config) =>
    {
        config.WriteTo.Console()
        .ReadFrom.Configuration(builder.Configuration);
    });

    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = ValidationErrorHelper.ValidationResponse;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddModules();

    var app = builder.Build();

    await app.Services.InitializeDatabasesAsync();
    app.UseInfrastructure(builder.Configuration);
    app.MapEndpoints();

    await app.RunAsync();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();

    Log.Information("Server Shutting down...");
    await Log.CloseAndFlushAsync();
}