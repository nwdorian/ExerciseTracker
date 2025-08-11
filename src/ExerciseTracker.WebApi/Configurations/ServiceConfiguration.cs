using Asp.Versioning;
using ExerciseTracker.Application;
using ExerciseTracker.Contracts;
using ExerciseTracker.Infrastructure;
using ExerciseTracker.WebApi.Exceptions;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace ExerciseTracker.WebApi.Configurations;

public static class ServiceConfiguration
{
    public static IServiceCollection AddWebApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        ConfigureApiVersioning(services);
        ConfigureFluentValidation(services);
        ConfigureGlobalExceptionHandling(services);

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

    private static void ConfigureFluentValidation(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ContractsAssembly.Assembly);

        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.DisableBuiltInModelValidation = true;
        });
    }

    private static void ConfigureGlobalExceptionHandling(IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails(configure =>
        {
            configure.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
            };
        });
    }
}
