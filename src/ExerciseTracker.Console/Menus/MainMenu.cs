using Spectre.Console;

namespace ExerciseTracker.Console.Menus;

public class MainMenu : IMenu
{
    private enum MenuOptions
    {
        ManageExercises,
        ManageCategories,
        Exit
    }

    public async Task Display()
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new FigletText("Exercise Tracker")
                    .LeftJustified()
                    .Color(Color.Yellow)
            );

            var selection = await AnsiConsole.PromptAsync(
                new SelectionPrompt<MenuOptions>()
                    .Title("Select from the menu:")
                    .UseConverter(GetMenuOptions)
                    .AddChoices(Enum.GetValues<MenuOptions>())
            );

            // selection switch
            // {
            //     MenuOptions.ManageExercises =>
            //     MenuOptions.ManageCategories => 
            //     MenuOptions.Exit => 
            // };
        }
    }

    private static string GetMenuOptions(MenuOptions options)
    {
        return options switch
        {
            MenuOptions.ManageExercises => "Manage Exercises",
            MenuOptions.ManageCategories => "Manage Categories",
            MenuOptions.Exit => "Exit",
            _ => options.ToString()
        };
    }
}
