namespace ExerciseTracker.Contracts.V1.Exercises.Requests;

public class CreateExerciseRequest
{
    public Guid CategoryId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Description { get; set; }
}
