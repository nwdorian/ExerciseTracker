namespace ExerciseTracker.Domain.Primitives;

public interface IAuditable
{
    DateTime DateCreated { get; set; }
    DateTime DateUpdated { get; set; }
}
