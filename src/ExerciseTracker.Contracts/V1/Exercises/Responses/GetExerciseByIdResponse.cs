namespace ExerciseTracker.Contracts.V1.Exercises.Responses;

public class GetExerciseByIdResponse
{
    public GetExerciseByIdResponse(ExerciseRecord exercise)
    {
        Exercise = exercise;
    }
    public ExerciseRecord Exercise { get; set; }
}
