using Core.Infrastructure.Util;
using Features.Weather.Domain.Queries;
using Features.Weather.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Weather;

[Route("api/v1/weather")]
public class WeatherController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public WeatherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<Result<WeatherConditionDto>> GetWeatherConditionDescription(
        [FromQuery] GetWeatherConditionDescriptionQuery conditionDescriptionQuery) =>
        _mediator.Send(conditionDescriptionQuery);
}