using System.Net;
using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure.Util;

public class SuccessResult<TSuccess> : Result<TSuccess>
{
    public TSuccess Value { get; }

    public SuccessResult(TSuccess val) : base(StatusCodes.Status200OK)
    {
        Value = val;
    }

    public SuccessResult(TSuccess val, int statusCode) : base(statusCode)
    {
        if (statusCode is <= 199 or >= 300)
            throw new InvalidOperationException("A SuccessResult must have a successful code");
        
        Value = val;
    }
}