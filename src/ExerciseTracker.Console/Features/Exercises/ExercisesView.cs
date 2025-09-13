using ExerciseTracker.Console.Common.Input;
using ExerciseTracker.Console.Features.Categories.Abstractions;
using ExerciseTracker.Console.Features.Exercises.Abstractions;
using ExerciseTracker.Contracts.V1.Exercises.Requests;
using Spectre.Console;

namespace ExerciseTracker.Console.Features.Exercises;

public class ExercisesView : IExercisesView
{
    private readonly IExercisesService _exercisesService;
    private readonly ICategoriesService _categoriesService;

    public ExercisesView(IExercisesService exercisesService, ICategoriesService categoriesService)
    {
        _exercisesService = exercisesService;
        _categoriesService = categoriesService;
    }
    public async Task GetAllExercises()
    {
        var exercises = await _exercisesService.GetAllExercises();
        if (exercises.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No exercises found[/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }
        ExercisesTables.DisplayExercises(exercises);
        UserInput.PromptAnyKeyToContinue();
    }
    public async Task CreateExercise()
    {
        AnsiConsole.MarkupLine("[blue]Creating a new exercise...[/]");

        var start = UserInput.PromptDateTime("Start date and time [grey](format: yyyy-MM-dd HH:mm)[/]:");
        var duration = UserInput.PromptDuration("Duration [grey](format: HH:mm)[/]:");
        var end = start.AddHours(duration.TotalHours);
        var description = UserInput.PromptString("Description [grey](leave empty to skip)[/]:", allowEmpty: true);

        var categories = await _categoriesService.GetAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No categories found. Create a category first![/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }
        var category = await UserInput.SelectCategory(categories);

        var exercise = new CreateExerciseRequest
        {
            CategoryId = category.Id,
            Start = start,
            End = end,
            Description = string.IsNullOrEmpty(description) ? null : description
        };

        ExercisesTables.DisplayCreateExerciseDetails(exercise, category);
        if (await AnsiConsole.ConfirmAsync("Are you sure you want to create a new exercise?"))
        {
            await _exercisesService.CreateExercise(exercise);
        }
    }
    public async Task DeleteExercise()
    {
        var exercises = await _exercisesService.GetAllExercises();
        if (exercises.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No exercises found[/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }
        ExercisesTables.DisplayExercises(exercises);

        var input = UserInput.PromptPositiveInteger("Enter exercise Id to delete [grey](or enter 0 to exit)[/]:", allowZero: true);
        var index = UserInput.GetValidListIndex(input, exercises);
        if (index == -1)
        {
            return;
        }
        var exercise = exercises.ElementAtOrDefault(index);
        if (exercise is null)
        {
            AnsiConsole.MarkupLine("[red]Exercise not found!");
            return;
        }

        ExercisesTables.DisplayExerciseDetails("Delete exercise", exercise);
        if (await AnsiConsole.ConfirmAsync("Are you sure you want to delete this exercise?"))
        {
            await _exercisesService.DeleteExercise(exercise.Id);
        }
    }
    public async Task UpdateExercise()
    {
        var exercises = await _exercisesService.GetAllExercises();
        if (exercises.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No exercises found[/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }
        ExercisesTables.DisplayExercises(exercises);

        var input = UserInput.PromptPositiveInteger("Enter exercise Id to update [grey]or enter 0 to exit[/]:", allowZero: false);
        var index = UserInput.GetValidListIndex(input, exercises);
        if (index == -1)
        {
            return;
        }
        var exercise = exercises.ElementAtOrDefault(index);
        if (exercise is null)
        {
            AnsiConsole.MarkupLine("[red]Exercise not found!");
            return;
        }

        ExercisesTables.DisplayExerciseDetails("Update exercise", exercise);
        AnsiConsole.MarkupLine("[grey]Enter new information.[/]");

        var start = UserInput.PromptDateTime("Start date and time [grey](format: yyyy-MM-dd HH:mm)[/]:");
        var duration = UserInput.PromptDuration("Duration [grey](format: HH:mm)[/]:");
        var end = start.AddHours(duration.TotalHours);
        var description = UserInput.PromptString("Description [grey](leave empty to skip)[/]:", allowEmpty: true);

        var categories = await _categoriesService.GetAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No categories found. Create a category first![/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }
        var category = await UserInput.SelectCategory(categories);

        var exerciseUpdate = new UpdateExerciseRequest()
        {
            CategoryId = category.Id,
            Start = start,
            End = end,
            Description = string.IsNullOrWhiteSpace(description) ? exercise.Description : description
        };

        ExercisesTables.DisplayUpdateExerciseDetails(exerciseUpdate, category);
        if (await AnsiConsole.ConfirmAsync("Are you sure you want to save the changes?"))
        {
            await _exercisesService.UpdateExercise(exercise.Id, exerciseUpdate);
        }
    }
}
