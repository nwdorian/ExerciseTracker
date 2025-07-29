using ExerciseTracker.Application.Contracts.Exercises;
using ExerciseTracker.Application.Contracts.Exercises.Commands;
using ExerciseTracker.Application.Contracts.Exercises.Queries;
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
    public async Task<Result<List<ExerciseDto>>> GetAll()
    {
        var result = await _exerciseRepository.GetAll();

        return Result.Success(result.Value.ToExerciseResponseList());
    }
    public async Task<Result<ExerciseDto>> GetById(GetExerciseQuery request)
    {
        var result = await _exerciseRepository.GetById(request.ExerciseId);

        return result.IsFailure
            ? result.Error
            : Result.Success(result.Value.ToExerciseResponse());
    }
    public async Task<Result<ExerciseDto>> Create(CreateExerciseCommand request)
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
            : Result.Success(exercise.ToExerciseResponse());
    }
    public async Task<Result> Delete(DeleteExerciseCommand request)
    {
        var getResult = await _exerciseRepository.GetById(request.ExerciseId);
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
    public async Task<Result> Update(UpdateExerciseCommand request)
    {
        var getExerciseResult = await _exerciseRepository.GetById(request.ExerciseId);
        if (getExerciseResult.IsFailure)
        {
            return getExerciseResult.Error;
        }

        var exercise = getExerciseResult.Value;

        if (!IsModified(exercise, request))
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

    private static bool IsModified(Exercise exercise, UpdateExerciseCommand request)
    {
        return exercise.Start != request.Start
            || exercise.End != request.End
            || exercise.Description != request.Description
            || exercise.CategoryId != request.CategoryId;
    }
}
