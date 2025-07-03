using System;

namespace ExerciseTracker.Domain.Primitives;

public class Error
{
    public Error(string message, string code)
    {
        Message = message;
        Code = code;
    }

    public string Message { get; set; }
    public string Code { get; set; }
}
