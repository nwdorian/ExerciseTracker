using ExerciseTracker.Domain.Primitives;

namespace ExerciseTracker.Domain.Models;

public class Exercise : ISoftDeletable, IAuditable
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Comments { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
