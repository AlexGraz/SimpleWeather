using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Util;
using Features.Weather.Domain.Dtos;
using Features.Weather.Domain.Queries;
using Features.Weather.Infrastructure;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace Test.Features.Weather.Queries;

public class WeatherQueryTests
{
    private readonly IWeatherService _weatherService = new WeatherService(new HttpClient());

    private async Task<Result<WeatherConditionDto>> RunGetWeatherConditionDescriptionQuery(string cityName, string countryName)
    {
        var queryHandler = new GetWeatherConditionDescriptionQueryHandler(_weatherService);
        return await queryHandler.Handle(
            new GetWeatherConditionDescriptionQuery(cityName, countryName),
            CancellationToken.None
        );
    }

    [Test]
    public async Task GetWeatherConditionDescriptionQuerySuccess()
    {
        var result = await RunGetWeatherConditionDescriptionQuery("Brisbane", "Australia");

        Assert.IsInstanceOf(typeof(SuccessResult<WeatherConditionDto>), result, "Return type was not SuccessResult");
        Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK, "Response status code was not 200 OK");
        Assert.IsFalse(
            string.IsNullOrEmpty(result.SuccessResult().Description),
            "The response Description was blank"
        );
    }

    [Test]
    public async Task GetWeatherConditionDescriptionNotFound()
    {
        var result = await RunGetWeatherConditionDescriptionQuery("AAAAAAAAAA", "AAAAAAAAAA");

        Assert.IsInstanceOf(typeof(FailResult<WeatherConditionDto>), result, "Returned result was not a FailResult");
        Assert.IsTrue(result.StatusCode == StatusCodes.Status404NotFound, "Response status code was not 404 Not Found");
    }
    
    [Test]
    public async Task GetWeatherConditionDescriptionInvalid()
    {
        var result = await RunGetWeatherConditionDescriptionQuery("", "");

        Assert.IsInstanceOf(typeof(FailResult<WeatherConditionDto>), result, "Returned result was not a FailResult");
        Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest, "Response status code was not 400 Bad Request");
    }
}