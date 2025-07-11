using ExerciseTracker.Application.Contracts.Exercises;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Application.Interfaces.Infrastructure;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Results;

namespace ExerciseTracker.Application.Services;

public class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ExerciseService(IExerciseRepository exerciseRepository, ICategoryRepository categoryRepository)
    {
        _exerciseRepository = exerciseRepository;
        _categoryRepository = categoryRepository;
    }
    public async Task<Result<List<ExerciseResponse>>> GetAll()
    {
        var result = await _exerciseRepository.GetAll();

        return Result.Success(result.Value.ToResponse());
    }
    public async Task<Result<ExerciseResponse>> GetById(Guid id)
    {
        var result = await _exerciseRepository.GetById(id);

        return result.IsFailure
            ? result.Error
            : Result.Success(result.Value.ToResponse());
    }
    public async Task<Result<ExerciseResponse>> Create(ExerciseRequest request)
    {
        var getCategoryResult = await _categoryRepository.GetById(request.CategoryId);
        if (getCategoryResult.IsFailure)
        {
            return getCategoryResult.Error;
        }
        var category = getCategoryResult.Value;

        var exercise = new Exercise(category.Id, request.Start, request.End, request.Description, category);

        var createResult = await _exerciseRepository.Create(exercise);

        return createResult.IsFailure
            ? createResult.Error
            : Result.Success(exercise.ToResponse());
    }
    public async Task<Result> Delete(Guid id)
    {
        var getResult = await _exerciseRepository.GetById(id);
        if (getResult.IsFailure)
        {
            return getResult.Error;
        }

        var exercise = getResult.Value;
        var deleteResult = await _exerciseRepository.Delete(exercise);

        return deleteResult.IsFailure
            ? deleteResult.Error
            : Result.Success();
    }
    public async Task<Result> Update(Guid id, ExerciseRequest request)
    {
        var getExerciseResult = await _exerciseRepository.GetById(id);
        if (getExerciseResult.IsFailure)
        {
            return getExerciseResult.Error;
        }

        var exercise = getExerciseResult.Value;

        if (!isModified(exercise, request))
        {
            return Result.Success();
        }

        if (exercise.CategoryId != request.CategoryId)
        {
            var getCategoryResult = await _categoryRepository.GetById(request.CategoryId);
            if (getCategoryResult.IsFailure)
            {
                return getCategoryResult.Error;
            }
            var category = getCategoryResult.Value;

            exercise.ChangeCategory(category);
        }

        exercise.ChangePeriod(request.Start, request.End);
        exercise.ChangeDescription(request.Description);

        var updateResult = await _exerciseRepository.Update(exercise);

        return updateResult.IsFailure
            ? updateResult.Error
            : Result.Success();
    }

    private static bool isModified(Exercise exercise, ExerciseRequest request)
    {
        return exercise.Start != request.Start
            || exercise.End != request.End
            || exercise.Description != request.Description
            || exercise.CategoryId != request.CategoryId;
    }
}
