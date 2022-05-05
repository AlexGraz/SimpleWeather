namespace Core.Weather.Infrastructure;

public interface IWeatherService
{
    public Task<WeatherConditionDto> GetConditionDescriptionAsync(string cityName, string countryName);
}