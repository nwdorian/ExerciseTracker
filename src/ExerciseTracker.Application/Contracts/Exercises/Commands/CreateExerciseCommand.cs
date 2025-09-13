namespace ExerciseTracker.Application.Contracts.Exercises.Commands;

public record class CreateExerciseCommand(Guid CategoryId, DateTime Start, DateTime End, string Description);
