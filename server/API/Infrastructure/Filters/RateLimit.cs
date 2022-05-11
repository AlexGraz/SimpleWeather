using System.Security.Claims;
using API.Infrastructure.Authentication;
using Core.Infrastructure.Util;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Infrastructure.Filters;

public class RateLimit : ActionFilterAttribute 
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var apiKeyValue = context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var apiKey = ApiKeyStore.Keys.Single(k => k.Key == apiKeyValue);
        
        if (apiKey.IsRateLimited())
        {
            context.Result = Result.Fail("API request limit reached", StatusCodes.Status429TooManyRequests);
        }
    }
}