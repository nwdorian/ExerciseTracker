using ExerciseTracker.Console.Common.Input;
using ExerciseTracker.Console.Features.Categories;
using ExerciseTracker.Console.Features.Exercises;
using Spectre.Console;

namespace ExerciseTracker.Console.Features;

public class MainMenu
{
    private readonly ExercisesMenu _exercisesMenu;
    private readonly CategoriesMenu _categoriesMenu;

    public MainMenu(
        ExercisesMenu exercisesMenu,
        CategoriesMenu categoriesMenu)
    {
        _exercisesMenu = exercisesMenu;
        _categoriesMenu = categoriesMenu;
    }

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

            switch (selection)
            {
                case MenuOptions.ManageExercises:
                    await _exercisesMenu.Display();
                    break;
                case MenuOptions.ManageCategories:
                    await _categoriesMenu.Display();
                    break;
                case MenuOptions.Exit:
                    exit = UserInput.ConfirmExit();
                    break;
            }
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
