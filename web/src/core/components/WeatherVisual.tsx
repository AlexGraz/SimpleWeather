import { WeatherCondition } from "code-gen/api-definitions";
import { WeatherConditionDescriptionsIcons } from "core/components/icons/WeatherConditionDescriptionsIcons";
import { useMemo } from "react";

export function WeatherVisual({
  weatherCondition,
}: {
  weatherCondition: WeatherCondition | undefined;
}) {
  return useMemo(() => {
    if (!weatherCondition) return <></>;

    const Icon = WeatherConditionDescriptionsIcons.find(
      (i) => i.description === weatherCondition?.description
    )?.icon;

    if (!Icon) throw new Error("Weather icon not found");

    return <Icon />;
  }, [weatherCondition]);
}
