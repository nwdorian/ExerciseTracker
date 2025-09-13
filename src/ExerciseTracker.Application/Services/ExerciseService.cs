using ExerciseTracker.Application.Contracts.Exercises;
using ExerciseTracker.Application.Contracts.Exercises.Commands;
using ExerciseTracker.Application.Contracts.Exercises.Queries;
using ExerciseTracker.Application.Extensions;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Application.Interfaces.Infrastructure;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Results;
using Microsoft.Extensions.Logging;

namespace ExerciseTracker.Application.Services;

public class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<ExerciseService> _logger;

    public ExerciseService(
        IExerciseRepository exerciseRepository,
        ICategoryRepository categoryRepository,
        ILogger<ExerciseService> logger)
    {
        _exerciseRepository = exerciseRepository;
        _categoryRepository = categoryRepository;
        _logger = logger;
    }
    public async Task<Result<List<ExerciseDto>>> GetAll()
    {
        var result = await _exerciseRepository.GetAll();

        _logger.LogServiceInfo(nameof(GetAll));
        return Result.Success(result.Value.ToExerciseResponseList());
    }
    public async Task<Result<ExerciseDto>> GetById(GetExerciseQuery request)
    {
        var result = await _exerciseRepository.GetById(request.ExerciseId);

        if (result.IsFailure)
        {
            _logger.LogServiceError(nameof(GetById), result.Error);
            return result.Error;
        }

        _logger.LogServiceInfo(nameof(GetById));
        return Result.Success(result.Value.ToExerciseResponse());

    }
    public async Task<Result<ExerciseDto>> Create(CreateExerciseCommand request)
    {
        var getCategoryResult = await _categoryRepository.GetById(request.CategoryId);
        if (getCategoryResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Create), getCategoryResult.Error);
            return getCategoryResult.Error;
        }
        var category = getCategoryResult.Value;

        var exercise = new Exercise(category.Id, request.Start, request.End, request.Description, category);

        var createResult = await _exerciseRepository.Create(exercise);

        if (createResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Create), createResult.Error);
            return createResult.Error;
        }

        _logger.LogServiceInfo(nameof(Create));
        return Result.Success(exercise.ToExerciseResponse());

    }
    public async Task<Result> Delete(DeleteExerciseCommand request)
    {
        var getResult = await _exerciseRepository.GetById(request.ExerciseId);
        if (getResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Delete), getResult.Error);
            return getResult.Error;
        }

        var exercise = getResult.Value;
        var deleteResult = await _exerciseRepository.Delete(exercise);

        if (deleteResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Delete), deleteResult.Error);
            return deleteResult.Error;
        }

        _logger.LogServiceInfo(nameof(Delete));
        return Result.Success();

    }
    public async Task<Result> Update(UpdateExerciseCommand request)
    {
        var getExerciseResult = await _exerciseRepository.GetById(request.ExerciseId);
        if (getExerciseResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Update), getExerciseResult.Error);
            return getExerciseResult.Error;
        }

        var exercise = getExerciseResult.Value;

        if (!IsModified(exercise, request))
        {
            _logger.LogServiceInfo(nameof(Update));
            return Result.Success();
        }

        if (exercise.CategoryId != request.CategoryId)
        {
            var getCategoryResult = await _categoryRepository.GetById(request.CategoryId);
            if (getCategoryResult.IsFailure)
            {
                _logger.LogServiceError(nameof(Update), getCategoryResult.Error);
                return getCategoryResult.Error;
            }
            var category = getCategoryResult.Value;

            exercise.ChangeCategory(category);
        }

        exercise.ChangePeriod(request.Start, request.End);
        exercise.ChangeDescription(request.Description);

        var updateResult = await _exerciseRepository.Update(exercise);

        if (updateResult.IsFailure)
        {
            _logger.LogServiceError(nameof(Update), updateResult.Error);
            return updateResult.Error;
        }

        _logger.LogServiceInfo(nameof(Update));
        return Result.Success();
    }

    private static bool IsModified(Exercise exercise, UpdateExerciseCommand request)
    {
        return exercise.Start != request.Start
            || exercise.End != request.End
            || exercise.Description != request.Description
            || exercise.CategoryId != request.CategoryId;
    }
}
