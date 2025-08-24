namespace ExerciseTracker.Contracts.V1.Categories.Responses;

public class GetAllCategoriesResponse
{
    public GetAllCategoriesResponse(List<CategoryRecord> categories)
    {
        Categories = categories;
    }
    public List<CategoryRecord> Categories { get; set; }
}
