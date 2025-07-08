using ExerciseTracker.Domain.Models;

namespace ExerciseTracker.Application.Contracts.Categories;

public static class CategoryMappings
{
    public static CategoryResponse ToResponse(this Category category)
        => new(category.Id, category.Name ?? "Unknown");

    public static List<CategoryResponse> ToResponse(this List<Category> categories)
        => new(categories.Select(c => c.ToResponse()));
}
