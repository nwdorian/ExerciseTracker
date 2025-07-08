namespace ExerciseTracker.Application.Contracts.Exercises;

public record class ExerciseRequest(Guid CategoryId, DateTime Start, DateTime End, string Description);
