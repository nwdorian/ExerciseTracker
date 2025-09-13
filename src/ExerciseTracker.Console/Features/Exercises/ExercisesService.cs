using System.Globalization;
using ExerciseTracker.Console.Common.Input;
using ExerciseTracker.Console.Features.Exercises.Abstractions;
using ExerciseTracker.Contracts.V1.Exercises;
using ExerciseTracker.Contracts.V1.Exercises.Requests;
using Spectre.Console;

namespace ExerciseTracker.Console.Features.Exercises;

public class ExercisesService : IExercisesService
{
    private readonly IExercisesClient _exercisesClient;

    public ExercisesService(IExercisesClient exercisesClient)
    {
        _exercisesClient = exercisesClient;
    }
    public async Task<List<ExerciseRecord>> GetAllExercises()
    {
        var exercises = new List<ExerciseRecord>();
        try
        {
            var response = await _exercisesClient.GetAllExercises();

            if (!response.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
                return exercises;
            }

            exercises = response.Content.Exercises;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
        return exercises;
    }
    public async Task CreateExercise(CreateExerciseRequest request)
    {
        try
        {
            var response = await _exercisesClient.CreateExercise(request);

            if (!response.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
                return;
            }

            AnsiConsole.MarkupLine("[green]Exercise created successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }
    public async Task DeleteExercise(Guid id)
    {
        try
        {
            var response = await _exercisesClient.DeleteExercise(id);

            if (!response.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
                return;
            }

            AnsiConsole.MarkupLine("[green]Exercise deleted successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }
    public async Task UpdateExercise(Guid id, UpdateExerciseRequest request)
    {
        try
        {
            var response = await _exercisesClient.UpdateExercise(id, request);

            if (!response.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
                return;
            }

            AnsiConsole.MarkupLine("[green]Exercise updated successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }
}
