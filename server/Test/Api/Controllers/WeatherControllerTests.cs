using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using API.Controllers.Weather;
using API.Infrastructure.Authentication.Attributes;
using API.Infrastructure.Filters;
using Core.Infrastructure.Util;
using Features.Weather.Domain.Dtos;
using Features.Weather.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Test.Api.Controllers;

public class WeatherControllerTests
{
    private WeatherController GetWeatherController(string weatherDescription = "extreme rain")
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.IsAny<GetWeatherConditionDescriptionQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<WeatherConditionDto>.Success(new WeatherConditionDto(weatherDescription)))
            .Verifiable("WeatherConditionDto was not sent");
        
        return new WeatherController(mediator.Object);
    }
    
    [Test]
    public async Task GetWeatherConditionDescriptionQuerySuccess()
    {
        const string weatherDescription = "very heavy rain";
        
        var controller = GetWeatherController(weatherDescription);
        var result = await controller.GetWeatherConditionDescription(new GetWeatherConditionDescriptionQuery("Brisbane", "Australia"));

        Assert.IsInstanceOf(typeof(SuccessResult<WeatherConditionDto>), result, "Return type was not SuccessResult");
        Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK, "Response status code was not 200 OK");
        Assert.IsTrue(
            result.SuccessResult().Description == weatherDescription,
            "Description is different from expected"
        );
    }
    
    [Test]
    public void ControllerCorrectAttributes()
    {
        var result = GetWeatherController();
        Assert.IsInstanceOf(typeof(ApiControllerBase), result, "WeatherController not an instance of ApiControllerBase");

        var attribute = typeof(WeatherController).GetCustomAttribute(typeof(RateLimit));
        Assert.IsNotNull(attribute, "RateLimit not found on WeatherController");
        
        attribute = typeof(WeatherController).GetCustomAttribute(typeof(AuthorizeApiKey));
        Assert.IsNotNull(attribute, "AuthorizeApiKey not found on WeatherController");
    }
}