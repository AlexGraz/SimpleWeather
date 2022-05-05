using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure.Util;

public class FailResult<TSuccess> : Result<TSuccess>
{
    public string Error { get; }
    public string Message { get; }

    public FailResult(string message): base(StatusCodes.Status400BadRequest)
    {
        Message = message;
    }
    
    public FailResult(string message, int statusCode): base(statusCode)
    {
        Message = message;
    }
}