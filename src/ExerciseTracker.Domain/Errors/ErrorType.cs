namespace ExerciseTracker.Domain.Errors;

public enum ErrorType
{
    None,
    NullValue,
    ConditionNotMet,
    Validation,
    NotFound,
    NotCreated,
    NotDeleted,
    NotUpdated
}
