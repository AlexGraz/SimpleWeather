using Core.Weather.Infrastructure.OpenWeather;

namespace Core.Weather.Infrastructure;

public class WeatherHttpClient : OpenWeatherHttpClient, IWeatherHttpClient
{
    public WeatherHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
}