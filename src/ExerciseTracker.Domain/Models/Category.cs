using ExerciseTracker.Domain.Primitives;

namespace ExerciseTracker.Domain.Models;

public sealed class Category : ISoftDeletable, IAuditable
{
    public Guid Id { get; }
    public string Name { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime DateUpdated { get; private set; }
    public ICollection<Exercise> Exercises { get; } = [];

    private Category() { }
    public Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name is required.");
        }

        Id = Guid.NewGuid();
        Name = name;
        IsActive = true;
    }
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.");
        }

        Name = name;
    }
    public void SoftDelete()
    {
        if (!IsActive)
        {
            return;
        }
        IsActive = false;
    }
    public void AuditCreation()
    {
        var now = DateTime.Now;

        DateCreated = now;
        DateUpdated = now;
    }
    public void AuditModification() => DateUpdated = DateTime.Now;
}
