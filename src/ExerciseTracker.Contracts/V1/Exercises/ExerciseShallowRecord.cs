namespace ExerciseTracker.Contracts.V1.Exercises;

public record class ExerciseShallowRecord(Guid Id, DateTime Start, DateTime End, TimeSpan Duration, string Description);
