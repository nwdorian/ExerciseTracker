using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Domain.Models;

namespace ExerciseTracker.Application.Contracts.Exercises;

public static class ExerciseMappings
{
    public static ExerciseResponse ToResponse(this Exercise exercise) =>
        new(exercise.Id,
            exercise.Start,
            exercise.End,
            exercise.Description ?? string.Empty,
            exercise.Category!.ToResponse());
    public static List<ExerciseResponse> ToResponse(this List<Exercise> exercises) =>
        exercises.Select(e => e.ToResponse()).ToList();
}
