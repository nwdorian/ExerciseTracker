using Asp.Versioning;
using ExerciseTracker.Application;
using ExerciseTracker.Infrastructure;

namespace ExerciseTracker.WebApi.Configurations;

public static class ServiceConfiguration
{
    public static IServiceCollection AddWebApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        ConfigureApiVersioning(services);

        services.AddControllers();

        services.AddOpenApi();

        return services;
    }

    private static void ConfigureApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}
