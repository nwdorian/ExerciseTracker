using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Application.Contracts.Categories.Commands;
using ExerciseTracker.Application.Contracts.Categories.Queries;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Application.Interfaces.Infrastructure;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Results;

namespace ExerciseTracker.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<List<CategoryShallowDto>>> GetAll()
    {
        var result = await _categoryRepository.GetAll();

        return Result.Success(result.Value.ToCategoryShallowResponseList());
    }
    public async Task<Result<CategoryDto>> GetById(GetCategoryQuery request)
    {
        var result = await _categoryRepository.GetById(request.CategoryId);

        return result.IsFailure
            ? result.Error
            : Result.Success(result.Value.ToCategoryResponse());
    }

    public async Task<Result<CategoryShallowDto>> Create(CreateCategoryCommand request)
    {
        var category = new Category(request.Name);

        var result = await _categoryRepository.Create(category);

        return result.IsFailure
            ? result.Error
            : Result.Success(category.ToCategoryShallowResponse());
    }

    public async Task<Result> Delete(DeleteCategoryCommand request)
    {
        var getResult = await _categoryRepository.GetById(request.CategoryId);
        if (getResult.IsFailure)
        {
            return getResult.Error;
        }

        var category = getResult.Value;
        var deleteResult = await _categoryRepository.Delete(category);

        return deleteResult.IsFailure
            ? deleteResult.Error
            : Result.Success();
    }

    public async Task<Result> Update(UpdateCategoryCommand request)
    {
        var getResult = await _categoryRepository.GetById(request.CategoryId);
        if (getResult.IsFailure)
        {
            return getResult.Error;
        }
        var category = getResult.Value;

        category.ChangeName(request.NewName);

        var updateResult = await _categoryRepository.Update(category);

        return updateResult.IsFailure
            ? updateResult.Error
            : Result.Success();
    }
}
