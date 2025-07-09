using ExerciseTracker.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.WebApi.Mappings;

public static class ErrorExtensions
{
    public static IActionResult ToActionResult(this Error error)
    {
        return error.Type switch
        {
            ErrorType.Validation => new BadRequestObjectResult(error),
            ErrorType.NotFound => new NotFoundObjectResult(error),
            _ => new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            }
        };
    }
}
