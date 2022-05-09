import { WeatherIcon } from "core/components/icons/WeatherIcon";

export function SunnyIcon() {
  return (
    <WeatherIcon>
      <div className="sun">
        <div className="rays"></div>
      </div>
    </WeatherIcon>
  );
}
