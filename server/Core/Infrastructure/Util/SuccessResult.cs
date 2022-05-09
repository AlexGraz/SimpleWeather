using Microsoft.AspNetCore.Http;
using TeeSquare.UnionTypes;

namespace Core.Infrastructure.Util;

public class SuccessResult<TSuccess> : Result<TSuccess>
{
    [AsConst(true)]
    public override bool IsSuccessful => true;
    public TSuccess Data { get; }

    public SuccessResult(TSuccess val) : base(StatusCodes.Status200OK)
    {
        Data = val;
    }

    public SuccessResult(TSuccess val, int statusCode) : base(statusCode)
    {
        if (statusCode is <= 199 or >= 300)
            throw new ArgumentException("A SuccessResult must have a successful code");
        
        Data = val;
    }
}