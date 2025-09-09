using ExerciseTracker.Contracts.V1.Categories.Requests;
using ExerciseTracker.Contracts.V1.Categories.Responses;

namespace ExerciseTracker.Console.Abstractions.Services;

public interface ICategoriesService
{
    Task<GetAllCategoriesResponse> GetAllCategories();
    Task<GetCategoryByIdResponse> GetCategoryById(Guid id);
    Task CreateCategory(CreateCategoryRequest request);
    Task DeleteCategory(Guid id);
    Task UpdateCategory(Guid id, UpdateCategoryRequest request);
}
