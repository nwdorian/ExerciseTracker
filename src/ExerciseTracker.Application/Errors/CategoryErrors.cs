using ExerciseTracker.Domain.Errors;

namespace ExerciseTracker.Application.Errors;

public static class CategoryErrors
{
    public static readonly Error NotCreated = new("Category.NotCreated", "Unable to create product", ErrorType.NotCreated);
    public static readonly Error NotFound = new("Category.NotFound", "Unable to find category with the requested Id", ErrorType.NotFound);
    public static readonly Error NotDeleted = new("Category.NotDeleted", "Unable to delete category with the requested Id", ErrorType.NotDeleted);
    public static readonly Error NotUpdated = new("Category.NotUpdated", "Unable to update category with the requested Id", ErrorType.NotUpdated);
}
