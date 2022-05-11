import { SunnyIcon } from "core/components/icons/SunnyIcon";
import { CloudyIcon } from "core/components/icons/CloudyIcon";
import { SunShowerIcon } from "core/components/icons/SunShowerIcon";
import { RainyIcon } from "core/components/icons/RainyIcon";
import { ThunderstormIcon } from "core/components/icons/ThunderstormIcon";
import { FlurriesIcon } from "core/components/icons/FlurriesIcon";

const WeatherConditionIcon = {
  ClearSky: SunnyIcon,
  FewClouds: CloudyIcon,
  ScatteredClouds: CloudyIcon,
  BrokenClouds: CloudyIcon,
  ShowerRain: SunShowerIcon,
  Rain: RainyIcon,
  Thunderstorm: ThunderstormIcon,
  Snow: FlurriesIcon,
  Mist: CloudyIcon,
};

type WeatherIcon = () => JSX.Element;

export const WeatherConditionDescriptionsIcons: {
  description: string;
  icon: WeatherIcon;
}[] = [];

const addIconDescriptions = (icon: WeatherIcon, descriptions: string[]) =>
  WeatherConditionDescriptionsIcons.push(
    ...descriptions.map((description) => ({
      description,
      icon,
    }))
  );

addIconDescriptions(WeatherConditionIcon.Thunderstorm, [
  "thunderstorm with light rain",
  "thunderstorm with rain",
  "thunderstorm with heavy rain",
  "light thunderstorm",
  "thunderstorm",
  "heavy thunderstorm",
  "ragged thunderstorm",
  "thunderstorm with light drizzle",
  "thunderstorm with drizzle",
  "thunderstorm with heavy drizzle",
]);

addIconDescriptions(WeatherConditionIcon.ShowerRain, [
  "light intensity drizzle",
  "drizzle",
  "heavy intensity drizzle",
  "light intensity drizzle rain",
  "drizzle rain",
  "heavy intensity drizzle rain",
  "shower rain and drizzle",
  "heavy shower rain and drizzle",
  "shower drizzle",
  "light intensity shower rain",
  "shower rain",
  "heavy intensity shower rain",
  "ragged shower rain",
]);

addIconDescriptions(WeatherConditionIcon.Rain, [
  "light rain",
  "moderate rain",
  "heavy intensity rain",
  "very heavy rain",
  "extreme rain",
]);

addIconDescriptions(WeatherConditionIcon.Snow, [
  "freezing rain",
  "light snow",
  "Snow",
  "Sleet",
  "Light shower sleet",
  "Shower sleet",
  "Light rain and snow",
  "Rain and snow",
  "Light shower snow",
  "Shower snow",
  "Heavy shower snow",
]);

addIconDescriptions(WeatherConditionIcon.Mist, [
  "mist",
  "Smoke",
  "Haze",
  "sand/ dust whirls",
  "fog",
  "sand",
  "dust",
  "volcanic ash",
  "squalls",
  "tornado",
]);

addIconDescriptions(WeatherConditionIcon.ClearSky, ["clear sky"]);
addIconDescriptions(WeatherConditionIcon.FewClouds, [
  "few clouds: 11-25%",
  "few clouds",
]);
addIconDescriptions(WeatherConditionIcon.ScatteredClouds, [
  "scattered clouds: 25-50%",
]);
addIconDescriptions(WeatherConditionIcon.BrokenClouds, [
  "broken clouds: 51-84%",
  "broken clouds",
  "overcast clouds: 85-100%",
  "overcast clouds",
]);
