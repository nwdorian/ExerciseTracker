namespace ExerciseTracker.Application.Contracts.Exercises.Commands;

public record class UpdateExerciseCommand(Guid ExerciseId, Guid CategoryId, DateTime Start, DateTime End, string Description);
