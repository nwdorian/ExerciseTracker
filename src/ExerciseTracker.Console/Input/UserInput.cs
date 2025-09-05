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
}
