import { WeatherIcon } from "core/components/icons/WeatherIcon";

export function ThunderstormIcon() {
  return (
    <WeatherIcon>
      <div className="cloud"></div>
      <div className="lightning">
        <div className="bolt"></div>
        <div className="bolt"></div>
      </div>
    </WeatherIcon>
  );
}
