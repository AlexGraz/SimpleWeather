namespace Features.Weather.Infrastructure.OpenWeather;

public record OpenWeatherResponseDto(
    OpenWeatherCoordinatesDto Coord,
    OpenWeatherWeatherDto[] Weather,
    OpenWeatherMainDto Main,
    int Visibility,
    OpenWeatherWindDto Wind,
    OpenWeatherCloudsDto Clouds,
    OpenWeatherVolumeDto? Rain,
    OpenWeatherVolumeDto? Snow,
    int Dt,
    OpenWeatherSystemDto System,
    int Timezone,
    int Id,
    string Name
);

public record OpenWeatherCoordinatesDto(
    float Lon,
    float Lat
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
    int Deg
);

public record OpenWeatherCloudsDto(
    int All
);

public record OpenWeatherVolumeDto(
    int VolumeOneHour,
    int VolumeThreeHours
);

public record OpenWeatherSystemDto(
    string Country,
    DateTimeOffset Sunrise,
    DateTimeOffset Sunset
);