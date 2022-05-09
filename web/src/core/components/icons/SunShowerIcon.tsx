import { WeatherIcon } from "core/components/icons/WeatherIcon";

export function SunShowerIcon() {
  return (
    <WeatherIcon>
      <div className="cloud" />
      <div className="sun">
        <div className="rays"></div>
      </div>
      <div className="rain"></div>
    </WeatherIcon>
  );
}
