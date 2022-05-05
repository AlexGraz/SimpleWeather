using Core.Infrastructure.Util;
using Core.Weather.Domain.Queries;
using Core.Weather.Infrastructure;
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
    public Task<Result<WeatherConditionDto>> GetCurrent(
        [FromQuery] GetCurrentWeatherQuery query) => _mediator.Send(query);
}