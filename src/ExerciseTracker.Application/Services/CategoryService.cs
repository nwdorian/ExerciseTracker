using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Application.Interfaces.Infrastructure;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Primitives;

namespace ExerciseTracker.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<List<CategoryResponse>>> GetAll()
    {
        var result = await _categoryRepository.GetAll();

        var response = result.Value.ToResponse();

        return Result.Success(response);
    }
    public async Task<Result<CategoryResponse>> GetById(Guid id)
    {
        var result = await _categoryRepository.GetById(id);

        if (result.IsFailure)
        {
            return Result.Failure<CategoryResponse>(result.Error);
        }

        var response = result.Value.ToResponse();
        return Result.Success(response);
    }

    public async Task<Result<CategoryResponse>> Create(CategoryRequest request)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            IsActive = true,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now
        };

        var result = await _categoryRepository.Create(category);
        return result.IsFailure
            ? Result.Failure<CategoryResponse>(result.Error)
            : Result.Success(category.ToResponse());
    }

    public async Task<Result> Delete(Guid id)
    {
        var getResult = await _categoryRepository.GetById(id);
        if (getResult.IsFailure)
        {
            return Result.Failure(getResult.Error);
        }

        var category = getResult.Value;
        var deleteResult = await _categoryRepository.Delete(category);
        return deleteResult.IsFailure
            ? Result.Failure(deleteResult.Error)
            : Result.Success();
    }

    public async Task<Result> Update(Guid id, CategoryRequest request)
    {
        var getResult = await _categoryRepository.GetById(id);
        if (getResult.IsFailure)
        {
            return Result.Failure(getResult.Error);
        }

        var category = getResult.Value;
        category.Name = request.Name;

        var updateResult = await _categoryRepository.Update(category);
        return updateResult.IsFailure
            ? Result.Failure(updateResult.Error)
            : Result.Success();
    }
}
