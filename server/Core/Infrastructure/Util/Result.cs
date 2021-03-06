using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeeSquare.UnionTypes;
using JetBrains.Annotations;

namespace Core.Infrastructure.Util;

[UnionType(typeof(SuccessResult<>), typeof(FailResult<>))]
public class Result<TSuccess> : Result
{
    public int? StatusCode { get; }
    
    [UsedImplicitly]
    public virtual bool IsSuccessful { get; }

    protected Result(int statusCode)
    {
        StatusCode = statusCode;
    }

    public override void ExecuteResult(ActionContext context)
    {
        var objectResult = new ObjectResult(this)
        {
            StatusCode = StatusCode
        };

        objectResult.ExecuteResultAsync(context);
    }

    public static implicit operator Result<TSuccess>(FailResult<Unit> failure)
    {
        return new FailResult<TSuccess>(failure.Message, failure.StatusCode ?? StatusCodes.Status400BadRequest);
    }

    public static implicit operator Result<TSuccess>(TSuccess successValue)
    {
        return new SuccessResult<TSuccess>(successValue);
    }

    public TSuccess SuccessResult() => this is SuccessResult<TSuccess> success
        ? success.Data
        : throw new InvalidOperationException("Result is not successful");

    public string FailMessage() => this is FailResult<TSuccess> failure
        ? failure.Message
        : throw new InvalidOperationException("Result is not fail");

    public string Serialize()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        return IsSuccessful
            ? JsonSerializer.Serialize((SuccessResult<TSuccess>) this, options)
            : JsonSerializer.Serialize((FailResult<TSuccess>) this, options);
    }
}