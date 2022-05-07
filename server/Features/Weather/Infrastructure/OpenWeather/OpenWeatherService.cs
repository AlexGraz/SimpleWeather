using System.Net.Http.Json;
using Microsoft.AspNetCore.Http.Extensions;

namespace Features.Weather.Infrastructure.OpenWeather;

public class OpenWeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private const string BaseUri = "https://api.openweathermap.org/data/2.5/weather";
    private const string Unit = "metric";
    private const string Language = "en";
    private const string ApiKey = "8b7535b42fe1c551f18028f64e8688f7";

    public OpenWeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUri);
    }

    public async Task<WeatherConditionDto> GetConditionDescriptionAsync(string cityName, string countryName)
    {
        var query = new QueryBuilder
        {
            { "q", $"{cityName},{countryName}" },
            { "units", Unit },
            { "lang", Language },
            { "appid", ApiKey }
        };

        var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponseDto>(query.ToString());

        if (response == null) throw new NullReferenceException("Response from OpenWeather was null");

        return new WeatherConditionDto(
            response.Weather.FirstOrDefault()?.Description ?? "No weather conditions found"
        );
    }
}