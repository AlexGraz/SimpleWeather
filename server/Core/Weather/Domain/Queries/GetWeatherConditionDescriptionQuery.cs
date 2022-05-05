using Core.Infrastructure.Util;
using Core.Weather.Infrastructure;
using MediatR;

namespace Core.Weather.Domain.Queries;

public record GetWeatherConditionDescriptionQuery(string CityName, string CountryName) : IRequest<Result<WeatherConditionDto>>;

public class GetWeatherConditionDescriptionQueryHandler : IRequestHandler<GetWeatherConditionDescriptionQuery, Result<WeatherConditionDto>>
{
    private IWeatherService _weatherService;

    public GetWeatherConditionDescriptionQueryHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<Result<WeatherConditionDto>> Handle(GetWeatherConditionDescriptionQuery request,
        CancellationToken cancellationToken)
    {
        return await _weatherService.GetConditionDescriptionAsync(request.CityName, request.CountryName);
    }
}