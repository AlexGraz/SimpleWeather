using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using TeeSquare.UnionTypes;

namespace Core.Infrastructure.Util;

public class FailResult<TSuccess> : Result<TSuccess>
{
    [AsConst(false)]
    public override bool IsSuccessful => false;
    public string Message { get; private set;  }

    public FailResult(string message): base(StatusCodes.Status400BadRequest)
    {
        Message = message;
    }
    
    public FailResult(string message, int statusCode): base(statusCode)
    {
        if (statusCode is >= 200 and <= 299)
            throw new ArgumentException("A FailResult must have a fail code");
        
        Message = message;
    }
}