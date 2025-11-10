using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PayStreamAggregator;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting Host");

    // Used Kestrel web server to host the health check endpoint
    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog as the host logger
    builder.Host.UseSerilog();

    builder.Services.AddHostedService<Worker>();

    builder.Services.AddHealthChecks()
        .AddCheck("self", () => HealthCheckResult.Healthy("The worker is healthy."));

    var app = builder.Build();

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    app.MapGet("/health/live", () => Results.Ok("Healthy"));

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Shutting down Host");
    Log.CloseAndFlush();
}