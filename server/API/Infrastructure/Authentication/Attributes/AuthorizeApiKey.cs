using API.Infrastructure.Authentication.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.Infrastructure.Authentication.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeApiKey : AuthorizeAttribute
{
    public AuthorizeApiKey()
    {
        AuthenticationSchemes = AuthScheme.ApiKey;
    }
}