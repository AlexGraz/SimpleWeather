namespace Core.Weather.Infrastructure;

public interface IWeatherHttpClient
{
    public Task<WeatherDto> GetCurrent(string cityName, string countryName);
}