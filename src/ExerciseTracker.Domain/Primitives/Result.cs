using System;

namespace ExerciseTracker.Domain.Primitives;

public class Result<T>
{
    public Result(T value)
    {
        Value = value;
        Error = null;
    }

    public Result(Error error)
    {
        Error = error;
        Value = default;
    }

    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error == null;

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(Error error) => new(error);

    public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure) => IsSuccess ? onSuccess(Value!) : onFailure(Error!);
}
