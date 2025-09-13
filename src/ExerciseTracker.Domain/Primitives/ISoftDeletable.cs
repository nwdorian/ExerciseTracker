namespace ExerciseTracker.Domain.Primitives;

public interface ISoftDeletable
{
    bool IsActive { get; }
    void SoftDelete();
}
