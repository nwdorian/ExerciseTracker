using ExerciseTracker.Application.Contracts.Categories;

namespace ExerciseTracker.Application.Contracts.Exercises;

public record class ExerciseResponse(Guid Id, DateTime Start, DateTime End, TimeSpan Duration, string Description, CategoryResponse Category);
