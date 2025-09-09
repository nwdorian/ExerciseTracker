using ExerciseTracker.Contracts.V1.Categories.Requests;
using ExerciseTracker.Contracts.V1.Categories.Responses;
using Refit;

namespace ExerciseTracker.Console.Features.Categories.Abstractions;

public interface ICategoriesClient
{
    [Get("/categories")]
    Task<ApiResponse<GetAllCategoriesResponse>> GetAllCategories();

    [Get("/categories/{id}")]
    Task<ApiResponse<GetCategoryByIdResponse>> GetCategoryById(Guid id);

    [Post("/categories")]
    Task<ApiResponse<CreateCategoryResponse>> CreateCategory([Body] CreateCategoryRequest request);

    [Delete("/categories/{id}")]
    Task<IApiResponse> DeleteCategory(Guid id);

    [Put("/categories/{id}")]
    Task<IApiResponse> UpdateCategory(Guid id, [Body] UpdateCategoryRequest request);
}
