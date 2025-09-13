using ExerciseTracker.Contracts.V1.Categories;
using ExerciseTracker.Contracts.V1.Categories.Requests;
using ExerciseTracker.Contracts.V1.Categories.Responses;

namespace ExerciseTracker.Console.Features.Categories.Abstractions;

public interface ICategoriesService
{
    Task<List<CategoryRecord>> GetAllCategories();
    Task<GetCategoryByIdResponse> GetCategoryById(Guid id);
    Task CreateCategory(CreateCategoryRequest request);
    Task DeleteCategory(Guid id);
    Task UpdateCategory(Guid id, UpdateCategoryRequest request);
}
