using ExerciseTracker.Infrastructure.Contexts;
using ExerciseTracker.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddDbContextWithSqlServer(this IServiceCollection services, IConfiguration configuration)
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

        return services;
    }
}
