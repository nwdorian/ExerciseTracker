namespace ExerciseTracker.Domain.Primitives;

public interface IAuditable
{
    DateTime DateCreated { get; }
    DateTime DateUpdated { get; }
    void AuditCreation();
    void AuditModification();
}
