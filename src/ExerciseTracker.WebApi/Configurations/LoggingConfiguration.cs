using Serilog;

namespace ExerciseTracker.WebApi.Configurations;

public static class LoggingConfiguration
{
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, config) =>
            config.ReadFrom.Configuration(context.Configuration));

        return builder;
    }
}
