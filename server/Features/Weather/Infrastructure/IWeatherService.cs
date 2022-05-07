namespace Features.Weather.Infrastructure;

public interface IWeatherService
{
    public Task<string> GetConditionDescriptionAsync(string cityName, string countryName);
}