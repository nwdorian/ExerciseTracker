using ExerciseTracker.Domain.Errors;

namespace ExerciseTracker.Application.Errors;

public static class ExerciseErrors
{
    public static readonly Error NotCreated = Error.Failure("Exercise.NotCreated", "Unable to create exercise");
    public static Error NotFound(Guid id)
        => Error.NotFound("Exercise.NotFound", $"Unable to find exercise with the Id = '{id}'");
    public static Error NotDeleted(Guid id)
        => Error.Failure("Exercise.NotDeleted", $"Unable to delete exercise with the Id = '{id}'");
    public static Error NotUpdated(Guid id)
        => Error.Failure("Exercise.NotUpdated", $"Unable to update exercise with the Id = '{id}'");
}
