using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Domain.Models;

namespace ExerciseTracker.Application.Contracts.Exercises;

public static class ExerciseMappings
{
    public static ExerciseShallowDto ToExerciseShallowResponse(this Exercise exercise)
        => new(exercise.Id,
            exercise.Start,
            exercise.End,
            exercise.Duration,
            exercise.Description ?? string.Empty);
    public static List<ExerciseShallowDto> ToExerciseShallowResponseList(this ICollection<Exercise> exercises)
        => exercises.Select(e => e.ToExerciseShallowResponse()).ToList();

    public static ExerciseDto ToExerciseResponse(this Exercise exercise)
        => new(exercise.Id,
        exercise.Start,
        exercise.End,
        exercise.Duration,
        exercise.Description ?? string.Empty,
        exercise.Category.ToCategoryShallowResponse());

    public static List<ExerciseDto> ToExerciseResponseList(this List<Exercise> exercises)
        => exercises.Select(e => e.ToExerciseResponse()).ToList();
}
