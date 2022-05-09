using Microsoft.AspNetCore.Authentication;

namespace API.Infrastructure.Authentication.Handlers;

public class ApiKeyAuthHandlerOptions : AuthenticationSchemeOptions
{
    public readonly string[] ApiKeys =
    {
        "1f2eb11d-6fa8-41de-89f7-f0b99adedba6", 
        "51c836f3-467c-40b5-a4bc-7f4d395c1abf",
        "b0fa898c-39a8-4bf0-b0bd-1560339af089", 
        "c30e5dfa-f257-476c-b54b-a46f414e4f03",
        "415185fc-53c0-4b3b-9645-565874213520"
    };
}