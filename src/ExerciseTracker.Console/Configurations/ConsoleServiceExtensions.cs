using ExerciseTracker.Console.Clients;
using ExerciseTracker.Console.Menus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ExerciseTracker.Console.Configurations;

public static class ConsoleServiceExtensions
{
    public static IServiceCollection AddConsoleServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMenuServices();
        services.ConfigureRefitClients(configuration);

        return services;
    }

    private static void ConfigureMenuServices(this IServiceCollection services)
    {
        services.AddTransient<MainMenu>();
        services.AddTransient<CategoriesMenu>();
        services.AddTransient<ExercisesMenu>();
    }

    private static void ConfigureRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        var apiAdress = configuration.GetValue<string>("ApiSettings:ExerciseTrackerApiAdress") ??
                            throw new InvalidOperationException();

        services.AddRefitClient<IExercisesClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiAdress));

        services.AddRefitClient<ICategoriesClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiAdress));
    }
}
