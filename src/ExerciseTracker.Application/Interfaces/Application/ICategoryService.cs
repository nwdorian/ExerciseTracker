using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Domain.Primitives;

namespace ExerciseTracker.Application.Interfaces.Application;

public interface ICategoryService
{
    Task<Result<List<CategoryResponse>>> GetAll();
    Task<Result<CategoryResponse>> GetById(Guid id);
    Task<Result<CategoryResponse>> Create(CategoryRequest request);
    Task<Result> Delete(Guid id);
    Task<Result> Update(Guid id, CategoryRequest request);
}
