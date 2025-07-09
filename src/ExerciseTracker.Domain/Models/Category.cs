using ExerciseTracker.Domain.Primitives;

namespace ExerciseTracker.Domain.Models;

public sealed class Category : ISoftDeletable, IAuditable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public ICollection<Exercise> Exercises { get; } = [];
}
