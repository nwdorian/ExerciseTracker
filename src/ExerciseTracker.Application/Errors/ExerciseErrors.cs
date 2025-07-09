using ExerciseTracker.Domain.Errors;

namespace ExerciseTracker.Application.Errors;

public static class ExerciseErrors
{
    public static readonly Error NotCreated = new("Exercise.NotCreated", "Unable to create exercise", ErrorType.NotCreated);
    public static readonly Error NotFound = new("Exercise.NotFound", "Unable to find exercise with the requested Id", ErrorType.NotFound);
    public static readonly Error NotDeleted = new("Exercise.NotDeleted", "Unable to delete exercise with the requested Id", ErrorType.NotDeleted);
    public static readonly Error NotUpdated = new("Exercise.NotUpdated", "Unable to update exercise with the requested Id", ErrorType.NotUpdated);
    public static readonly Error NotModified = new("Exercise.NotModified", "No modifications to update", ErrorType.NotModified);
}
