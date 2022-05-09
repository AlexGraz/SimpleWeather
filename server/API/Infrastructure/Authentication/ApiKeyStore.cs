namespace API.Infrastructure.Authentication;

public static class ApiKeyStore
{
    public static ApiKey[] Keys { get; private set; } = Array.Empty<ApiKey>();

    public static void InitKeys(IEnumerable<string> keys, int requestLimit, double requestLimitPeriodHours)
    {
        Keys = keys.Select(k => new ApiKey(k, requestLimit, requestLimitPeriodHours)).ToArray();
    }
}