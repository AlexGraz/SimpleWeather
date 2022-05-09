import React from "react";
import styled from "styled-components";
import { WeatherBody } from "core/components/WeatherBody";
import { WeatherHeaderBar } from "core/components/WeatherHeaderBar";
import { useWeatherApiRequester } from "core/hooks/useWeatherApiRequester";
import { Api } from "core/api/Api";

const Container = styled.div`
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  flex-direction: column;
`;

export function WeatherPage() {
  const { data, getData } = useWeatherApiRequester(
    Api.Weather.GetWeatherConditionDescription
  );

  return (
    <Container>
      <WeatherHeaderBar onFinish={getData} />
      <WeatherBody weatherCondition={data?.data} />
    </Container>
  );
}
