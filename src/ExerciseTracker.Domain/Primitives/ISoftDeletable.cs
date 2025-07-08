namespace ExerciseTracker.Domain.Primitives;

public interface ISoftDeletable
{
    bool IsActive { get; set; }
}
