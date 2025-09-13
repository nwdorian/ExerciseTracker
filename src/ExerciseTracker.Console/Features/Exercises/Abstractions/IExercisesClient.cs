using ExerciseTracker.Contracts.V1.Exercises.Requests;
using ExerciseTracker.Contracts.V1.Exercises.Responses;
using Refit;

namespace ExerciseTracker.Console.Features.Exercises.Abstractions;

public interface IExercisesClient
{
    [Get("/exercises")]
    Task<ApiResponse<GetAllExercisesResponse>> GetAllExercises();

    [Get("/exercises/{id}")]
    Task<ApiResponse<GetExerciseByIdResponse>> GetExerciseById(Guid id);

    [Post("/exercises")]
    Task<IApiResponse> CreateExercise([Body] CreateExerciseRequest request);

    [Delete("/exercises/{id}")]
    Task<IApiResponse> DeleteExercise(Guid id);

    [Put("/exercises/{id}")]
    Task<IApiResponse> UpdateExercise(Guid id, [Body] UpdateExerciseRequest request);
}
