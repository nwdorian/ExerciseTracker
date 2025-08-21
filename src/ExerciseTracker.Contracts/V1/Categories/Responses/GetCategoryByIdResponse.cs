using ExerciseTracker.Contracts.V1.Exercises;

namespace ExerciseTracker.Contracts.V1.Categories.Responses;

public class GetCategoryByIdResponse
{
    public GetCategoryByIdResponse(Guid id, string name, List<ExerciseShallowRecord> exercises)
    {
        Id = id;
        Name = name;
        Exercises = exercises;
    }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<ExerciseShallowRecord> Exercises { get; set; }
}
