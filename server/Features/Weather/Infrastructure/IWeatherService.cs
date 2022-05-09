using Core.Infrastructure.Util;
using Features.Weather.Domain.Dtos;

namespace Features.Weather.Infrastructure;

public interface IWeatherService
{
    public Task<Result<WeatherConditionDto>> GetConditionDescriptionAsync(string cityName, string countryName);
}