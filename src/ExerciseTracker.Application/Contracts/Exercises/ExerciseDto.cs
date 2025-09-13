using ExerciseTracker.Application.Contracts.Categories;

namespace ExerciseTracker.Application.Contracts.Exercises;

public record class ExerciseDto(Guid Id, DateTime Start, DateTime End, TimeSpan Duration, string Description, CategoryShallowDto Category);
