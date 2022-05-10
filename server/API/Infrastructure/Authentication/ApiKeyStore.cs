namespace API.Infrastructure.Authentication;

public class AuthOptions
{
    public const string Auth = "Auth";
    public int RequestLimit { get; set; }
    public int RequestLimitPeriodHours { get; set; }
    public string[] Keys { get; set; } = Array.Empty<string>();
}

public static class ApiKeyStore
{
    public static ApiKey[] Keys { get; private set; } = Array.Empty<ApiKey>();
    
    public static void InitKeys(IConfiguration configuration)
    {
        var authOptions = configuration.GetSection(AuthOptions.Auth).Get<AuthOptions>();
        Keys = authOptions.Keys
            .Select(k => new ApiKey(k, authOptions.RequestLimit, authOptions.RequestLimitPeriodHours))
            .ToArray();
    }

    public static void InitKeys(IEnumerable<string> keys, int requestLimit, double requestLimitPeriodHours)
    {
        Keys = keys.Select(k => new ApiKey(k, requestLimit, requestLimitPeriodHours)).ToArray();
    }
    
    public static void AddKey(ApiKey key)
    {
        var array = Keys;
        Array.Resize(ref array, Keys.Length + 1);
        array[^1] = key;
        Keys = array;
    }
}