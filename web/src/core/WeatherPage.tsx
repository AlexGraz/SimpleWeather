import React from "react";
import styled from "styled-components";
import { WeatherBody } from "core/components/WeatherBody";
import { WeatherHeaderBar } from "core/components/WeatherHeaderBar";
import { useWeatherApiRequester } from "core/hooks/useWeatherApiRequester";
import { Api } from "core/api/Api";
import { ErrorMessage } from "core/components/ErrorMessage";

const Container = styled.div`
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  flex-direction: column;
`;

export function WeatherPage() {
  const { data, getData, loading, error } = useWeatherApiRequester(
    Api.Weather.GetWeatherConditionDescription
  );

  return (
    <Container>
      <WeatherHeaderBar onSubmit={getData} />
      <ErrorMessage error={error} />
      <WeatherBody weatherCondition={data} loading={loading} />
    </Container>
  );
}
