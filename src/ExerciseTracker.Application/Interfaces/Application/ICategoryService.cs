using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Application.Contracts.Categories.Commands;
using ExerciseTracker.Application.Contracts.Categories.Queries;
using ExerciseTracker.Domain.Results;

namespace ExerciseTracker.Application.Interfaces.Application;

public interface ICategoryService
{
    Task<Result<List<CategoryShallowDto>>> GetAll();
    Task<Result<CategoryDto>> GetById(GetCategoryQuery request);
    Task<Result<CategoryShallowDto>> Create(CreateCategoryCommand request);
    Task<Result> Delete(DeleteCategoryCommand request);
    Task<Result> Update(UpdateCategoryCommand request);
}
