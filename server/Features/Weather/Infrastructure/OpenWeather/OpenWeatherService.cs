using System.Net;
using System.Net.Http.Json;
using Core.Infrastructure.Util;
using Features.Weather.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
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

    public async Task<Result<WeatherConditionDto>> GetConditionDescriptionAsync(string cityName, string countryName)
    {
        OpenWeatherResponseDto? response;
        var query = new QueryBuilder
        {
            { "q", $"{cityName},{countryName}" },
            { "units", Unit },
            { "lang", Language },
            { "appid", ApiKey }
        };

        try
        {
            response = await _httpClient.GetFromJsonAsync<OpenWeatherResponseDto>(query.ToString());
        }
        catch (HttpRequestException e)
        {
            return e.StatusCode switch
            {
                HttpStatusCode.NotFound => Result.Fail(
                    "City and/or country not found, please try again",
                    StatusCodes.Status404NotFound
                ),
                HttpStatusCode.BadRequest => Result.Fail(
                    "An invalid request was sent to OpenWeather",
                    StatusCodes.Status400BadRequest
                ),
                _ => Result.Fail(
                    "An unknown error occured",
                    e.StatusCode == null ? StatusCodes.Status500InternalServerError : (int)e.StatusCode
                )
            };
        }

        if (response == null) throw new NullReferenceException("Response from OpenWeather was null");

        return new WeatherConditionDto(response.Weather.FirstOrDefault()?.Description ?? "No weather conditions found");
    }
}