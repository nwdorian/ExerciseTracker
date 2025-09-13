using ExerciseTracker.Application.Contracts.Exercises;
using ExerciseTracker.Application.Contracts.Exercises.Commands;
using ExerciseTracker.Application.Contracts.Exercises.Queries;
using ExerciseTracker.Domain.Results;

namespace ExerciseTracker.Application.Interfaces.Application;

public interface IExerciseService
{
    Task<Result<List<ExerciseDto>>> GetAll();
    Task<Result<ExerciseDto>> GetById(GetExerciseQuery request);
    Task<Result<ExerciseDto>> Create(CreateExerciseCommand request);
    Task<Result> Delete(DeleteExerciseCommand request);
    Task<Result> Update(UpdateExerciseCommand request);
}
