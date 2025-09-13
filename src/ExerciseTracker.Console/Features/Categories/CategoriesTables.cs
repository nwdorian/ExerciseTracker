using System.Globalization;
using ExerciseTracker.Console.Common.Constants;
using ExerciseTracker.Contracts.V1.Categories;
using ExerciseTracker.Contracts.V1.Categories.Requests;
using ExerciseTracker.Contracts.V1.Categories.Responses;
using Spectre.Console;

namespace ExerciseTracker.Console.Features.Categories;

public static class CategoriesTables
{
    public static void DisplayCategoriesTable(List<CategoryRecord> categories)
    {
        var table = new Table
        {
            Title = new TableTitle("[blue]Categories[/]", "bold")
        };
        table.AddColumn(new TableColumn("[deepskyblue1]Id[/]"));
        table.AddColumn(new TableColumn("[deepskyblue1]Name[/]"));

        foreach (var (index, category) in categories.Index())
        {
            table.AddRow((index + 1).ToString(CultureInfo.InvariantCulture), category.Name);
        }

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    public static void DisplayCategoryExercises(GetCategoryByIdResponse category)
    {
        var exercisesTable = new Table()
        {
            Border = TableBorder.MinimalHeavyHead
        };
        exercisesTable.AddColumn(new TableColumn("[deepskyblue1]Date[/]").Centered());
        exercisesTable.AddColumn(new TableColumn("[deepskyblue1]Start[/]").Centered());
        exercisesTable.AddColumn(new TableColumn("[deepskyblue1]End[/]").Centered());
        exercisesTable.AddColumn(new TableColumn("[deepskyblue1]Duration[/]").Centered());
        exercisesTable.AddColumn(new TableColumn("[deepskyblue1]Description[/]").Centered());

        foreach (var exercise in category.Exercises)
        {
            exercisesTable.AddRow(
                exercise.Start.ToString(Formatting.DateDisplay, CultureInfo.InvariantCulture),
                exercise.Start.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
                exercise.End.ToString(Formatting.TimeDisplay, CultureInfo.InvariantCulture),
                exercise.Duration.ToFormattedDuration(),
                string.IsNullOrWhiteSpace(exercise.Description) ? "No description" : exercise.Description
                );
        }

        var panel = new Panel(exercisesTable)
        {
            Header = new PanelHeader($"[blue]{category.Name}[/]")
        };
        panel.Header.Centered();
        AnsiConsole.Clear();
        AnsiConsole.Write(panel);
    }

    public static void DisplayCreateCategoryDetails(CreateCategoryRequest category)
    {
        var table = new Table();
        table.AddColumn(new TableColumn("[deepskyblue1]Category name[/]").Centered());
        table.AddRow(category.Name!);

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    public static void DisplayCategoryDetails(CategoryRecord category)
    {
        var table = new Table();
        table.AddColumn(new TableColumn("[deepskyblue1]Category name[/]").Centered());
        table.AddRow(category.Name!);

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
