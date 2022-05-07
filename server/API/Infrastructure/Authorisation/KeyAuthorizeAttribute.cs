using Core.Infrastructure.Util;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Infrastructure.Authorisation;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class KeyAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public KeyAuthorizeAttribute()
    {
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authKey = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        var apiKey = ApiKeyStore.Keys.FirstOrDefault(k => k.Key == authKey);

        if (apiKey == null)
        {
            context.Result = new FailResult<Unit>("API key not valid", StatusCodes.Status401Unauthorized);
            return;
        }

        if (!apiKey.Validate())
        {
            context.Result = new FailResult<Unit>("API key rate limited", StatusCodes.Status429TooManyRequests);
        }
    }
}