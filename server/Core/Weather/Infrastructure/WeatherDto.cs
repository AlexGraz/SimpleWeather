namespace Core.Weather.Infrastructure;

public record WeatherDto(
    string? Main,
    string? Description,
    float Temperature,
    float TemperatureMax,
    float TemperatureMin
);