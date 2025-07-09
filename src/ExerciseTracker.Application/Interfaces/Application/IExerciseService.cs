using ExerciseTracker.Application.Contracts.Exercises;
using ExerciseTracker.Domain.Results;

namespace ExerciseTracker.Application.Interfaces.Application;

public interface IExerciseService
{
    Task<Result<List<ExerciseResponse>>> GetAll();
    Task<Result<ExerciseResponse>> GetById(Guid id);
    Task<Result<ExerciseResponse>> Create(ExerciseRequest request);
    Task<Result> Delete(Guid id);
    Task<Result> Update(Guid id, ExerciseRequest request);
}
