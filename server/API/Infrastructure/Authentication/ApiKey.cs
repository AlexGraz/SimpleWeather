namespace API.Infrastructure.Authentication;

public class ApiKey
{
    public readonly string Key;
    public int RequestCount { get; private set; }
    public DateTime? FirstRequestDateTime { get; private set; }

    public readonly double RequestLimitPeriodHours;
    public readonly int RequestLimit;

    public ApiKey(string key, int requestLimit, double requestLimitPeriodHours)
    {
        Key = key;
        RequestLimitPeriodHours = requestLimitPeriodHours;
        RequestLimit = requestLimit;
    }

    public bool IsRateLimited()
    {
        if (FirstRequestDateTime != null 
            && (DateTime.Now - FirstRequestDateTime).Value.TotalHours >= RequestLimitPeriodHours)
        {
            FirstRequestDateTime = DateTime.Now;
            RequestCount = 1;
            return false;
        }

        FirstRequestDateTime ??= DateTime.Now;

        RequestCount++;
        return RequestCount > RequestLimit;
    }
};