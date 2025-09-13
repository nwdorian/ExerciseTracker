namespace ExerciseTracker.Contracts.V1.Exercises.Responses;

public class GetAllExercisesResponse
{
    public GetAllExercisesResponse(List<ExerciseRecord> exercises)
    {
        Exercises = exercises;
    }
    public List<ExerciseRecord> Exercises { get; set; }
}
