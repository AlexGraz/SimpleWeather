namespace API.Infrastructure.Authorisation;

public class AuthOptions
{
    public const string Auth = "Auth";
    public int RequestLimit { get; set; }
    public int LimitResetHours { get; set; }
    public string[] Keys { get; set; } = Array.Empty<string>();
}

public static class ApiKeyStore
{
    public static ApiKey[] Keys { get; private set; } = Array.Empty<ApiKey>();

    public static void InitKeysFromConfiguration(IConfiguration configuration)
    {
        var authOptions = configuration.GetSection(AuthOptions.Auth).Get<AuthOptions>();
        Keys = authOptions.Keys.Select(k => new ApiKey(k, authOptions.LimitResetHours, authOptions.RequestLimit))
            .ToArray();
    }
}