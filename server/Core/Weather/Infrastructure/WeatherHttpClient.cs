using Core.Weather.Infrastructure.OpenWeather;

namespace Core.Weather.Infrastructure;

public class WeatherHttpClient : OpenWeatherHttpClient
{
    public WeatherHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
}