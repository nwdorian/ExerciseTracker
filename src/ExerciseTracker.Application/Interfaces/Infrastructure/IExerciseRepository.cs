using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Results;

namespace ExerciseTracker.Application.Interfaces.Infrastructure;

public interface IExerciseRepository
{
    Task<Result<List<Exercise>>> GetAll();
    Task<Result<Exercise>> GetById(Guid id);
    Task<Result> Create(Exercise exercise);
    Task<Result> Delete(Exercise exercise);
    Task<Result> Update(Exercise exercise);
}
