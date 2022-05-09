import { WeatherIcon } from "core/components/icons/WeatherIcon";

export function RainyIcon() {
  return (
    <WeatherIcon>
      <div className="cloud"></div>
      <div className="rain"></div>
    </WeatherIcon>
  );
}
