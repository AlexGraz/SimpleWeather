using Features.Weather.Infrastructure.OpenWeather;

namespace Features.Weather.Infrastructure;

public class WeatherService : OpenWeatherService
{
    public WeatherService(HttpClient httpClient) : base(httpClient)
    {
    }
}