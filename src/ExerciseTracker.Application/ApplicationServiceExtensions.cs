using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IExerciseService, ExerciseService>();

        return services;
    }
}
