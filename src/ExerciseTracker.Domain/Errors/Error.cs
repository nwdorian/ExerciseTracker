using System.Text.Json.Serialization;

namespace ExerciseTracker.Domain.Errors;

public record class Error(string Code, string Message, [property: JsonIgnore] ErrorType type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.", ErrorType.NullValue);

    public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.", ErrorType.ConditionNotMet);
}
