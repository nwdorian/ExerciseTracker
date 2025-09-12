using ExerciseTracker.Console.Features;
using ExerciseTracker.Console.Features.Categories;
using ExerciseTracker.Console.Features.Categories.Abstractions;
using ExerciseTracker.Console.Features.Exercises;
using ExerciseTracker.Console.Features.Exercises.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;

namespace ExerciseTracker.Console.Configurations;

public static class ConsoleServiceExtensions
{
    public static IServiceCollection AddConsoleServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(b => b.ClearProviders());
        services.ConfigureMenuServices();
        services.ConfigureApplicationServices();
        services.ConfigureViewServices();
        services.ConfigureRefitClients(configuration);

        return services;
    }

    private static void ConfigureMenuServices(this IServiceCollection services)
    {
        services.AddTransient<MainMenu>();
        services.AddTransient<CategoriesMenu>();
        services.AddTransient<ExercisesMenu>();
    }

    private static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICategoriesService, CategoriesService>();
        services.AddTransient<IExercisesService, ExercisesService>();
    }

    private static void ConfigureViewServices(this IServiceCollection services)
    {
        services.AddTransient<ICategoriesView, CategoriesView>();
        services.AddTransient<IExercisesView, ExercisesView>();
    }

    private static void ConfigureRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        var apiAddress = configuration.GetValue<string>("ApiSettings:ExerciseTrackerApiAddress") ??
                            throw new InvalidOperationException();

        services.AddRefitClient<IExercisesClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiAddress));

        services.AddRefitClient<ICategoriesClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiAddress));
    }
}
