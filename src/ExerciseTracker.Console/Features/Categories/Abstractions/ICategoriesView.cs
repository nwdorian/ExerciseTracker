namespace ExerciseTracker.Console.Features.Categories.Abstractions;

public interface ICategoriesView
{
    Task GetAllCategories();
    Task CreateCategory();
    Task DeleteCategory();
    Task UpdateCategory();
}
