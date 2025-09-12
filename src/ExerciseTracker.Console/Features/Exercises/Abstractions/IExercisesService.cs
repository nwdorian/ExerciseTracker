using ExerciseTracker.Contracts.V1.Exercises;
using ExerciseTracker.Contracts.V1.Exercises.Requests;

namespace ExerciseTracker.Console.Features.Exercises.Abstractions;

public interface IExercisesService
{
    Task<List<ExerciseRecord>> GetAllExercises();
    Task CreateExercise(CreateExerciseRequest request);
    Task DeleteExercise(Guid id);
    Task UpdateExercise(Guid id, UpdateExerciseRequest request);
}
