﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TeeSquare.UnionTypes;

namespace Core.Infrastructure.Util;

[UnionType(typeof(SuccessResult<>), typeof(FailResult<>))]
public class Result<TSuccess> : ActionResult
{
    public int? StatusCode { get; }

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
        ? success.Data
        : throw new InvalidOperationException("Result is not successful");
    
    public string FailMessage() => this is FailResult<TSuccess> failure
        ? failure.Message
        : throw new InvalidOperationException("Result is not fail");
}