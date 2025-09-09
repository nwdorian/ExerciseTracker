namespace ExerciseTracker.Console.Abstractions.Views;

public interface ICategoriesView
{
    Task GetAllCategories();
    Task CreateCategory();
    Task DeleteCategory();
    Task UpdateCategory();
}
