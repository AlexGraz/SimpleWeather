using Microsoft.AspNetCore.Http.Extensions;

namespace Core.Weather.Infrastructure.OpenWeather;

public class OpenWeatherHttpClient: IWeatherHttpClient
{
    private readonly HttpClient _httpClient;
    private const string BaseUri = "https://api.openweathermap.org/data/2.5/weather";
    private const string Unit = "metric";
    private const string Language = "en";
    private const string ApiKey = "8b7535b42fe1c551f18028f64e8688f7";

    public OpenWeatherHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUri);
    }

    public async Task<WeatherDto> GetCurrent(string cityName, string countryName)
    {
        var query = new QueryBuilder
        {
            { "q", $"{cityName},{countryName}" },
            { "units", Unit },
            { "lang", Language },
            { "appid", ApiKey }
        };
        
        // return await _httpClient.GetFromJsonAsync<OpenWeatherResponseDto>(query.ToString());
        return new WeatherDto();
    }
}