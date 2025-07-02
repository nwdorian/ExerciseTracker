using ExerciseTracker.Domain.Primitives;

namespace ExerciseTracker.Domain.Models;

public sealed class Exercise : ISoftDeletable, IAuditable
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Comments { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public Category? Category { get; set; }
}
