namespace API.Infrastructure.Authorisation;

public class ApiKey
{
    public readonly string Key;
    public int RequestCount { get; private set; }
    public DateTime? FirstRequestDateTime { get; private set; }

    public readonly int LimitResetHours;
    public readonly int RequestLimit;

    public ApiKey(string key, int limitResetHours, int requestLimit)
    {
        Key = key;
        LimitResetHours = limitResetHours;
        RequestLimit = requestLimit;
    }

    public bool Validate()
    {
        RequestCount++;
        FirstRequestDateTime ??= DateTime.Now;

        if (RequestCount <= RequestLimit)
        {
            return true;
        }

        if ((DateTime.Now - FirstRequestDateTime).Value.TotalHours >= LimitResetHours)
        {
            FirstRequestDateTime = DateTime.Now;
            RequestCount = 1;
            return true;
        }

        return false;
    }
};