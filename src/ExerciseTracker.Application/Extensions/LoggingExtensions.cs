using ExerciseTracker.Domain.Errors;
using Microsoft.Extensions.Logging;

namespace ExerciseTracker.Application.Extensions;

public static class LoggingExtensions
{
    public static void LogServiceInfo<T>(
        this ILogger<T> logger,
        string method,
        string message = "Completed request")
        => logger.LogInformation(
            "{Message} {Service}.{Method} at {DateTime}",
            message,
            typeof(T).Name,
            method,
            DateTime.Now);

    public static void LogServiceError<T>(
        this ILogger<T> logger,
        string method,
        Error error,
        string message = "Request failure"
        )
        => logger.LogError(
            "{Message} {Service}.{Method} at {DateTime} {@Error}",
            message,
            typeof(T).Name,
            method,
            DateTime.Now,
            error
        );
}
