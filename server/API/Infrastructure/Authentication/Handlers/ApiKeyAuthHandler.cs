using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace API.Infrastructure.Authentication.Handlers;

public class ApiKeyAuthHandlerOptions : AuthenticationSchemeOptions
{
}

public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthHandlerOptions>
{
    public const string SchemeName = "ApiKey";

    public ApiKeyAuthHandler(
        IOptionsMonitor<ApiKeyAuthHandlerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
    ) : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers["Authorization"];

        if (authorizationHeader.Count == 0)
        {
            return await Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
        }

        var authHeader = AuthenticationHeaderValue.Parse(authorizationHeader);

        var apiKey = ApiKeyStore.Keys.FirstOrDefault(k => k.Key == authHeader.Parameter);

        if (apiKey == null)
        {
            return await Task.FromResult(AuthenticateResult.Fail("Invalid API key"));
        }

        var identity = new ClaimsIdentity(Scheme.Name);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, apiKey.Key, ClaimValueTypes.String, "ApiKey"));

        var principal = new GenericPrincipal(identity, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }
}