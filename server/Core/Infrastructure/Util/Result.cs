using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Infrastructure.Util;

public record ErrorResponseDto(
    ErrorDto Error
);

public record ErrorDto(
    string Message
);

public class Result<TSuccess> : IActionResult
{
    public int StatusCode { get; }
    public bool IsSuccessful => StatusCode is >= 200 and <= 299;

    protected Result(int statusCode)
    {
        StatusCode = statusCode;
    }
    
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(IsSuccessful
            ? SuccessResult()
            : new ErrorResponseDto(new ErrorDto(FailMessage()))
        )
        {
            StatusCode = StatusCode
        };

        await objectResult.ExecuteResultAsync(context);
    }

    public static Result<T> Success<T>(T value)
    {
        return new SuccessResult<T>(value);
    }
    
    public static Result<T> Success<T>(T value, int statusCode)
    {
        return new SuccessResult<T>(value, statusCode);
    }
    
    public static FailResult<Unit> Fail(string message)
    {
        return new FailResult<Unit>(message);
    }
    
    public static Result<T> Fail<T>(string message)
    {
        return new FailResult<T>(message);
    }
    
    public static implicit operator Result<TSuccess>(FailResult<Unit> failure)
    {
        return new FailResult<TSuccess>(failure.Message);
    }
    
    public static implicit operator Result<TSuccess>(TSuccess successValue)
    {
        return new SuccessResult<TSuccess>(successValue);
    }
    
    public TSuccess SuccessResult() => this is SuccessResult<TSuccess> success
        ? success.Value
        : throw new InvalidOperationException("Result is not successful");
    
    public string FailMessage() => this is FailResult<TSuccess> failure
        ? failure.Message
        : throw new InvalidOperationException("Result is not fail");
}