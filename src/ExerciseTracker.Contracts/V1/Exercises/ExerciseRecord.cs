using ExerciseTracker.Contracts.V1.Categories;

namespace ExerciseTracker.Contracts.V1.Exercises;

public record class ExerciseRecord(Guid Id, DateTime Start, DateTime End, TimeSpan Duration, string Description, CategoryRecord Category);
