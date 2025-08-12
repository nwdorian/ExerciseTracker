using ExerciseTracker.WebApi.Configurations;
using Serilog;

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

    app.Run();

    Log.Information("Application starting");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
