namespace ExerciseTracker.Contracts.V1.Categories.Responses;

public class CreateCategoryResponse
{
    public CreateCategoryResponse(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string? Name { get; set; }
}
