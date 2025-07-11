using ExerciseTracker.Domain.Primitives;

namespace ExerciseTracker.Domain.Models;

public sealed class Exercise : ISoftDeletable, IAuditable
{
    public Guid Id { get; }
    public Guid CategoryId { get; private set; }
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public TimeSpan Duration => End - Start;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime DateUpdated { get; private set; }
    public Category Category { get; private set; } = null!;

    private Exercise() { }
    public Exercise(Guid categoryId, DateTime start, DateTime end, string? description, Category category)
    {
        if (start >= end)
        {
            throw new ArgumentException("Start time must be before End time.");
        }

        Id = Guid.NewGuid();
        CategoryId = categoryId;
        Category = category ?? throw new ArgumentNullException(nameof(category));
        Start = start;
        End = end;
        Description = description;
        IsActive = true;
    }
    public void ChangeCategory(Category category)
    {
        Category = category ?? throw new ArgumentNullException(nameof(category));
        CategoryId = category.Id;
    }
    public void ChangePeriod(DateTime start, DateTime end)
    {
        if (start >= end)
        {
            throw new ArgumentException("Start time must be before End time.");
        }

        Start = start;
        End = end;
    }
    public void ChangeDescription(string? description) => Description = description;
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
