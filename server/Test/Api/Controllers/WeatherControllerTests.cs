using System.Threading;
using System.Threading.Tasks;
using API.Controllers.Weather;
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
    [Test]
    public async Task GetWeatherConditionDescriptionQuerySuccess()
    {
        const string weatherDescription = "very heavy rain";

        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.IsAny<GetWeatherConditionDescriptionQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<WeatherConditionDto>.Success(new WeatherConditionDto(weatherDescription)))
            .Verifiable("WeatherConditionDto was not sent");
        
        var controller = new WeatherController(mediator.Object);
        var result = await controller.GetWeatherConditionDescription(new GetWeatherConditionDescriptionQuery("Brisbane", "Australia"));

        Assert.IsInstanceOf(typeof(SuccessResult<WeatherConditionDto>), result, "Return type was not SuccessResult");
        Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK, "Response status code was not 200 OK");
        Assert.IsTrue(
            result.SuccessResult().Description == weatherDescription,
            "Description is different from expected"
        );
    }
}