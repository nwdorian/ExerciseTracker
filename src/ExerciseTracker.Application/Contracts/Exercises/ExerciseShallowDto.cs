namespace ExerciseTracker.Application.Contracts.Exercises;

public record class ExerciseShallowDto(Guid Id, DateTime Start, DateTime End, TimeSpan Duration, string Description);
