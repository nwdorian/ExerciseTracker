using Serilog;

namespace ExerciseTracker.WebApi.Configurations;

public static class LoggingConfiguration
{
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

        return builder;
    }
}
