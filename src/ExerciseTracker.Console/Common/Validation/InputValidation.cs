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

    public static ValidationResult IsValidDateTime(DateTime input)
    {
        if (input > DateTime.Now)
        {
            return ValidationResult.Error("[red]Date can't be in the future![/]");
        }

        return ValidationResult.Success();
    }

    public static ValidationResult IsValidDuration(TimeSpan input)
    {
        if (input <= TimeSpan.Zero)
        {
            return ValidationResult.Error("[red]Duration must be longer then 0![/]");
        }

        if (input >= TimeSpan.FromDays(1))
        {
            return ValidationResult.Error("[red]Duration can't be longer then 24h![/]");
        }

        return ValidationResult.Success();
    }
}
