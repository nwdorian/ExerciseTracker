using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Application.Contracts.Categories.Commands;
using ExerciseTracker.Application.Contracts.Categories.Queries;
using ExerciseTracker.Application.Extensions;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Application.Interfaces.Infrastructure;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Results;
using Microsoft.Extensions.Logging;

namespace ExerciseTracker.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<Result<List<CategoryShallowDto>>> GetAll()
    {
        var result = await _categoryRepository.GetAll();

        _logger.LogServiceInfo(nameof(GetAll));
        return Result.Success(result.Value.ToCategoryShallowResponseList());
    }
    public async Task<Result<CategoryDto>> GetById(GetCategoryQuery request)
    {
        var result = await _categoryRepository.GetById(request.CategoryId);

        if (result.IsFailure)
        {
            _logger.LogServiceError(nameof(GetById), result.Error);
            return result.Error;
        }

        _logger.LogServiceInfo(nameof(GetById));
        return Result.Success(result.Value.ToCategoryResponse());

    }

    public async Task<Result<CategoryShallowDto>> Create(CreateCategoryCommand request)
    {
        var category = new Category(request.Name);

        var result = await _categoryRepository.Create(category);

        if (result.IsFailure)
        {
            _logger.LogServiceError(nameof(Create), result.Error);
            return result.Error;
        }

        _logger.LogServiceInfo(nameof(Create));
        return Result.Success(category.ToCategoryShallowResponse());

    }

    public async Task<Result> Delete(DeleteCategoryCommand request)
    {
        var getResult = await _categoryRepository.GetById(request.CategoryId);
        if (getResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Delete), getResult.Error);
            return getResult.Error;
        }

        var category = getResult.Value;
        var deleteResult = await _categoryRepository.Delete(category);

        if (deleteResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Delete), deleteResult.Error);
            return deleteResult.Error;
        }

        _logger.LogServiceInfo(nameof(Delete));
        return Result.Success();

    }

    public async Task<Result> Update(UpdateCategoryCommand request)
    {
        var getResult = await _categoryRepository.GetById(request.CategoryId);
        if (getResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Update), getResult.Error);
            return getResult.Error;
        }
        var category = getResult.Value;

        category.ChangeName(request.NewName);

        var updateResult = await _categoryRepository.Update(category);

        if (updateResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Update), updateResult.Error);
            return updateResult.Error;
        }

        _logger.LogServiceInfo(nameof(Update));
        return Result.Success();
    }
}
