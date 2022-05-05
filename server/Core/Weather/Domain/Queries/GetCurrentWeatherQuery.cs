using Core.Infrastructure.Util;
using Core.Weather.Infrastructure;
using MediatR;

namespace Core.Weather.Domain.Queries;

public record GetCurrentWeatherQuery(string CityName, string CountryName) : IRequest<Result<WeatherConditionDto>>;

public class GetCurrentWeatherQueryHandler : IRequestHandler<GetCurrentWeatherQuery, Result<WeatherConditionDto>>
{
    private IWeatherService _weatherService;

    public GetCurrentWeatherQueryHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<Result<WeatherConditionDto>> Handle(GetCurrentWeatherQuery request,
        CancellationToken cancellationToken)
    {
        return await _weatherService.GetConditionDescriptionAsync(request.CityName, request.CountryName);
    }
}