using ExerciseTracker.Console.Common.Validation;
using Spectre.Console;

namespace ExerciseTracker.Console.Input;

public static class UserInput
{
    public static bool ConfirmExit()
    {
        if (AnsiConsole.Confirm("Are you sure you want to exit?"))
        {
            AnsiConsole.WriteLine("Goodbye!");
            return true;
        }
        return false;
    }

    public static void PromptAnyKeyToContinue()
    {
        AnsiConsole.Write("Press any key to continue...");
        System.Console.ReadLine();
    }

    public static int PromptPositiveInteger(string displayMessage, bool allowZero)
    {
        var prompt = new TextPrompt<int>(displayMessage);
        prompt.ValidationErrorMessage("[red]Input must a positive integer![/]");

        if (allowZero)
        {
            prompt.Validate(InputValidation.IsGreaterThanOrEqualToZero);
        }
        else
        {
            prompt.Validate(InputValidation.IsGreaterThanZero);
        }

        return AnsiConsole.Prompt(prompt);
    }

    public static int GetValidListIndex<T>(int input, List<T> list) where T : class
    {
        while (input > list.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid input![/]");
            input = PromptPositiveInteger("Enter record Id (or enter 0 to exit):", allowZero: true);
        }
        return input - 1;
    }

    public static string PromptString(string displayMessage, bool allowEmpty)
    {
        var prompt = new TextPrompt<string>(displayMessage);

        if (allowEmpty)
        {
            prompt.AllowEmpty();
        }

        return AnsiConsole.Prompt(prompt);
    }
}
