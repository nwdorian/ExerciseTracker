using Spectre.Console;

namespace ExerciseTracker.Console.Features.Exercises;

public class ExercisesMenu
{
    private enum MenuOptions
    {
        GetAll,
        Add,
        Delete,
        Update,
        MainMenu
    }

#pragma warning disable CA1822 // Mark members as static
#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
    public async Task Display()
#pragma warning restore S2325 // Methods and properties that don't access instance data should be static
#pragma warning restore CA1822 // Mark members as static
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            var selection = await AnsiConsole.PromptAsync(
                new SelectionPrompt<MenuOptions>()
                    .Title("Select from the menu:")
                    .UseConverter(GetMenuOptions)
                    .AddChoices(Enum.GetValues<MenuOptions>())
            );

            switch (selection)
            {
                case MenuOptions.GetAll:
                    break;
                case MenuOptions.Add:
                    break;
                case MenuOptions.Delete:
                    break;
                case MenuOptions.Update:
                    break;
                case MenuOptions.MainMenu:
                    exit = true;
                    break;
            }
        }
    }

    private static string GetMenuOptions(MenuOptions options)
    {
        return options switch
        {
            MenuOptions.GetAll => "Get All Exercises",
            MenuOptions.Add => "Add Exercise",
            MenuOptions.Delete => "Delete Exercise",
            MenuOptions.Update => "Update Exercise",
            MenuOptions.MainMenu => "Main Menu",
            _ => options.ToString()
        };
    }
}
