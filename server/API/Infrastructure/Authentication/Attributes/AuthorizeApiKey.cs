using API.Infrastructure.Authentication.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace API.Infrastructure.Authentication.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeApiKey : AuthorizeAttribute
{
    public AuthorizeApiKey()
    {
        AuthenticationSchemes = ApiKeyAuthHandler.SchemeName;
    }
}