using Core.Weather.Infrastructure.OpenWeather;

namespace Core.Weather.Infrastructure;

public class WeatherService : OpenWeatherService
{
    public WeatherService(HttpClient httpClient) : base(httpClient)
    {
    }
}