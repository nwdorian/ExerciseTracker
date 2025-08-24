using System.Globalization;
using ExerciseTracker.WebApi.Configurations;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

try
{
    Log.Information("Creating host");

    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureSerilog();

    Log.Information("Configuring services");

    builder.Services.AddWebApplicationServices(builder.Configuration);

    var app = builder.Build();

    Log.Information("Configuring middleware");

    await app.UseWebApplicationMiddleware();

    Log.Information("Application starting");

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
