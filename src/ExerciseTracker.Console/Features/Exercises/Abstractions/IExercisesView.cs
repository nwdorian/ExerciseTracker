namespace ExerciseTracker.Console.Features.Exercises.Abstractions;

public interface IExercisesView
{
    Task GetAllExercises();
    Task CreateExercise();
    Task DeleteExercise();
    Task UpdateExercise();
}
