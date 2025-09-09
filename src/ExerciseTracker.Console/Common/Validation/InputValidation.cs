using Spectre.Console;

namespace ExerciseTracker.Console.Common.Validation;

public static class InputValidation
{
    public static ValidationResult IsGreaterThanOrEqualToZero(int input)
    {
        return input switch
        {
            < 0 => ValidationResult.Error("[red]Input must be greater than or equal to zero![/]"),
            _ => ValidationResult.Success()
        };
    }

    public static ValidationResult IsGreaterThanZero(int input)
    {
        return input switch
        {
            < 1 => ValidationResult.Error("[red]Input must be greater than 0![/]"),
            _ => ValidationResult.Success()
        };
    }
}
