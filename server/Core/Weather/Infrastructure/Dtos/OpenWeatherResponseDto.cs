namespace Core.Weather.Infrastructure.Dtos;

public record OpenWeatherResponseDto(
    OpenWeatherCoordinatesDto Coordinates,
    OpenWeatherWeatherDto[] Weather,
    string Base,
    OpenWeatherMainDto Main,
    int Visibility,
    OpenWeatherWindDto Wind,
    OpenWeatherCloudsDto Clouds,
    OpenWeatherVolumeDto? Rain,
    OpenWeatherVolumeDto? Snow,
    DateTimeOffset TimeOfDataCalculation,
    OpenWeatherSystemDto System,
    TimeZoneInfo TimeZone,
    int Id,
    string Name,
    int Cod
);

public record OpenWeatherCoordinatesDto(
    string Longitude,
    string Latitude
);

public record OpenWeatherWeatherDto(
    int Id,
    string Main,
    string Description,
    string Icon
);

public record OpenWeatherMainDto(
    float Temp,
    float FeelsLike,
    float TempMin,
    float TempMax,
    int Pressure,
    int Humidity
);

public record OpenWeatherWindDto(
    float Speed,
    int DirectionDegrees
);

public record OpenWeatherCloudsDto(
    int Cloudiness
);

public record OpenWeatherVolumeDto(
    int VolumeOneHour,
    int VolumeThreeHours
);

public record OpenWeatherSystemDto(
    int Type,
    int Id,
    string Country,
    DateTimeOffset Sunrise,
    DateTimeOffset Sunset
);