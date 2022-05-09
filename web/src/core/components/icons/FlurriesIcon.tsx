import { WeatherIcon } from "core/components/icons/WeatherIcon";

export function FlurriesIcon() {
  return (
    <WeatherIcon>
      <div className="cloud"></div>
      <div className="snow">
        <div className="flake"></div>
        <div className="flake"></div>
      </div>
    </WeatherIcon>
  );
}
