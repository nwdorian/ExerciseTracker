using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.WebApi.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        //TODO add logging

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An error occured",
                Detail = exception.Message
            }
        });
    }
}
