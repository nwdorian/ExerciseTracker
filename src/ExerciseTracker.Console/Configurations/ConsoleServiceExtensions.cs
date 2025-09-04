using ExerciseTracker.Console.Configurations.Enums;
using ExerciseTracker.Console.Menus;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Console.Configurations;

public static class ConsoleServiceExtensions
{
    public static IServiceCollection AddConsoleServices(this IServiceCollection services)
    {
        services.ConfigureMenuServices();

        return services;
    }

    private static IServiceCollection ConfigureMenuServices(this IServiceCollection services)
    {
        services.AddKeyedTransient<IMenu, MainMenu>(MenuType.Main);

        return services;
    }
}
