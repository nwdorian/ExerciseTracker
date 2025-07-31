using ExerciseTracker.Application.Interfaces.Infrastructure;
using ExerciseTracker.Infrastructure.Contexts;
using ExerciseTracker.Infrastructure.Interceptors;
using ExerciseTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddDbContextWithSqlServer(services, configuration);
        AddInfrastructure(services);

        return services;
    }

    private static void AddDbContextWithSqlServer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddSingleton<UpdateAuditableInterceptor>();

        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ExerciseTrackerContext>(
            (serviceProvider, options) =>
            {
                var softDeleteInterceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();
                var updateAuditableInterceptor = serviceProvider.GetRequiredService<UpdateAuditableInterceptor>();

                options.UseSqlServer(connectionString)
                    .AddInterceptors(
                        softDeleteInterceptor,
                        updateAuditableInterceptor);
            }
        );
    }

    private static void AddInfrastructure(IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
    }
}
