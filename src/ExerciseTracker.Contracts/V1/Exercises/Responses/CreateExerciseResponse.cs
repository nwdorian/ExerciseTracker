using ExerciseTracker.Contracts.V1.Categories;

namespace ExerciseTracker.Contracts.V1.Exercises.Responses;

public class CreateExerciseResponse
{
    public CreateExerciseResponse(Guid id, DateTime start, DateTime end, TimeSpan duration, string description, CategoryRecord category)
    {
        Id = id;
        Start = start;
        End = end;
        Duration = duration;
        Description = description;
        Category = category;
    }
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan Duration { get; set; }
    public string? Description { get; set; }
    public CategoryRecord Category { get; set; }
}
