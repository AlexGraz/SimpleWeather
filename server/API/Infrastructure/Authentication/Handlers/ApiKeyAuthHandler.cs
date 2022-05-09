using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace API.Infrastructure.Authentication.Handlers;

public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthHandlerOptions>
{
    public ApiKeyAuthHandler(
        IOptionsMonitor<ApiKeyAuthHandlerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
    ) : base(options, logger, encoder, clock)
    {
        if (ApiKeyStore.Keys.Length != 0) return;
        var apiKeyOptions = options.CurrentValue;
        ApiKeyStore.InitKeys(
            apiKeyOptions.ApiKeys,
            ApiKeyAuthHandlerOptions.DefaultRequestLimit,
            ApiKeyAuthHandlerOptions.DefaultRequestLimitPeriodHours
        );
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

        var apiKey = ApiKeyStore.Keys.FirstOrDefault(k => k.Key == authHeader.Parameter);

        if (apiKey == null)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key"));
        }

        var identity = new ClaimsIdentity(Scheme.Name);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, apiKey.Key, ClaimValueTypes.String, "ApiKey"));

        var principal = new GenericPrincipal(identity, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}