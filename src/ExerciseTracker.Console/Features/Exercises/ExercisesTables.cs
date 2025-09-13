using System.Globalization;
using ExerciseTracker.Console.Common.Constants;
using ExerciseTracker.Contracts.V1.Categories;
using ExerciseTracker.Contracts.V1.Exercises;
using ExerciseTracker.Contracts.V1.Exercises.Requests;
using Spectre.Console;

namespace ExerciseTracker.Console.Features.Exercises;

public static class ExercisesTables
{
    public static void DisplayExercises(List<ExerciseRecord> exercises)
    {
        var table = new Table()
        {
            Title = new TableTitle("[blue]Exercises[/]", "bold"),
            ShowRowSeparators = true
        };
        table.AddColumn(new TableColumn("[deepskyblue1]Id[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Date[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Start[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]End[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Duration[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Description[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Category[/]").Centered());

        foreach (var (index, exercise) in exercises.Index())
        {
            table.AddRow(
                (index + 1).ToString(CultureInfo.InvariantCulture),
                exercise.Start.ToString(Formatting.DateDisplay, CultureInfo.InvariantCulture),
                exercise.Start.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
                exercise.End.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
                exercise.Duration.ToFormattedDuration(),
                string.IsNullOrWhiteSpace(exercise.Description) ? "No description" : exercise.Description,
                exercise.Category.Name
            );
        }

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    public static void DisplayCreateExerciseDetails(CreateExerciseRequest exercise, CategoryRecord category)
    {
        var table = new Table
        {
            Title = new TableTitle("[blue]New exercise[/]")
        };
        table.AddColumn(new TableColumn("[deepskyblue1]Date[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Start[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]End[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Duration[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Description[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Category[/]").Centered());

        table.AddRow(
            exercise.Start.ToString(Formatting.DateDisplay, CultureInfo.InvariantCulture),
            exercise.Start.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
            exercise.End.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
            (exercise.End - exercise.Start).ToFormattedDuration(),
            string.IsNullOrWhiteSpace(exercise.Description) ? "No description" : exercise.Description,
            category.Name
        );

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    public static void DisplayExerciseDetails(string title, ExerciseRecord exercise)
    {
        var table = new Table
        {
            Title = new TableTitle($"[blue]{title}[/]")
        };
        table.AddColumn(new TableColumn("[deepskyblue1]Date[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Start[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]End[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Duration[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Description[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Category[/]").Centered());

        table.AddRow(
            exercise.Start.ToString(Formatting.DateDisplay, CultureInfo.InvariantCulture),
            exercise.Start.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
            exercise.End.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
            exercise.Duration.ToFormattedDuration(),
            string.IsNullOrWhiteSpace(exercise.Description) ? "No description" : exercise.Description,
            exercise.Category.Name
        );

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    public static void DisplayUpdateExerciseDetails(UpdateExerciseRequest exercise, CategoryRecord category)
    {
        var table = new Table
        {
            Title = new TableTitle("[blue]Updated exercise[/]")
        };
        table.AddColumn(new TableColumn("[deepskyblue1]Date[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Start[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]End[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Duration[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Description[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Category[/]").Centered());

        table.AddRow(
            exercise.Start.ToString(Formatting.DateDisplay, CultureInfo.InvariantCulture),
            exercise.Start.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
            exercise.End.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
            (exercise.End - exercise.Start).ToFormattedDuration(),
            string.IsNullOrWhiteSpace(exercise.Description) ? "No description" : exercise.Description,
            category.Name
        );

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    private static string ToFormattedDuration(this TimeSpan duration)
    {
        var parts = new List<string>();

        if (duration.Days > 0)
        {
            parts.Add($"{duration.Days}d");
        }

        if (duration.Hours > 0)
        {
            parts.Add($"{duration.Hours}h");
        }

        if (duration.Minutes > 0)
        {
            parts.Add($"{duration.Minutes}m");
        }

        if (parts.Count == 0)
        {
            parts.Add("0m");
        }

        return string.Join(" ", parts);
    }
}
