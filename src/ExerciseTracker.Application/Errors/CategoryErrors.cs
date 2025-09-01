using ExerciseTracker.Domain.Errors;

namespace ExerciseTracker.Application.Errors;

public static class CategoryErrors
{
    public static readonly Error NotCreated = Error.Failure("Category.NotCreated", "Unable to create product");
    public static Error NotFound(Guid id)
        => Error.NotFound("Category.NotFound", $"Unable to find category with Id = '{id}'");
    public static Error NotDeleted(Guid id)
        => Error.Failure("Category.NotDeleted", $"Unable to delete category with Id = '{id}'");
    public static Error NotUpdated(Guid id)
        => Error.Failure("Category.NotUpdated", $"Unable to update category with Id = '{id}'");
}
