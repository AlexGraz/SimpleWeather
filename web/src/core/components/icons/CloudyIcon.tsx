import { WeatherIcon } from "core/components/icons/WeatherIcon";

export function CloudyIcon() {
  return (
    <WeatherIcon>
      <div className="cloud"></div>
      <div className="cloud"></div>
    </WeatherIcon>
  );
}
