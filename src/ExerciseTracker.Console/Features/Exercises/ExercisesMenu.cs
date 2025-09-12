using ExerciseTracker.Console.Features.Exercises.Abstractions;
using Spectre.Console;

namespace ExerciseTracker.Console.Features.Exercises;

public class ExercisesMenu
{
    private readonly IExercisesView _exercisesView;

    public ExercisesMenu(IExercisesView exercisesView)
    {
        _exercisesView = exercisesView;
    }
    private enum MenuOptions
    {
        GetAll,
        Add,
        Delete,
        Update,
        MainMenu
    }

    public async Task Display()
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
                    await _exercisesView.GetAllExercises();
                    break;
                case MenuOptions.Add:
                    await _exercisesView.CreateExercise();
                    break;
                case MenuOptions.Delete:
                    await _exercisesView.DeleteExercise();
                    break;
                case MenuOptions.Update:
                    await _exercisesView.UpdateExercise();
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
