using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Infrastructure.Util;

public abstract class Result : ActionResult
{
    public static SuccessResult<T> Success<T>(T value)
    {
        return new SuccessResult<T>(value);
    }

    public static SuccessResult<T> Success<T>(T value, int statusCode)
    {
        return new SuccessResult<T>(value, statusCode);
    }
    
    public static FailResult<Unit> Fail(string message)
    {
        return new FailResult<Unit>(message);
    }
    
    public static FailResult<Unit> Fail(string message, int statusCode)
    {
        return new FailResult<Unit>(message, statusCode);
    }
}
    