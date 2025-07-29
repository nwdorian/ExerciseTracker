using ExerciseTracker.Application.Contracts.Exercises;
using ExerciseTracker.Domain.Models;

namespace ExerciseTracker.Application.Contracts.Categories;

public static class CategoryMappings
{
    public static CategoryShallowDto ToCategoryShallowResponse(this Category category)
        => new(category.Id, category.Name);

    public static List<CategoryShallowDto> ToCategoryShallowResponseList(this List<Category> categories)
        => categories.Select(c => c.ToCategoryShallowResponse()).ToList();

    public static CategoryDto ToCategoryResponse(this Category category)
        => new(category.Id, category.Name, category.Exercises.ToExerciseShallowResponseList());
}
