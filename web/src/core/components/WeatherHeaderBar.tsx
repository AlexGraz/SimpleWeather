import styled from "styled-components";
import { WeatherForm } from "core/components/WeatherForm";
import React from "react";
import { GetWeatherConditionDescriptionQuery } from "code-gen/api-definitions";

const Container = styled.div`
  height: 4rem;
  width: 100%;
  display: flex;
  align-items: center;
  background-color: white;
  padding: 1rem;
`;

export function WeatherHeaderBar({
  onFinish,
}: {
  onFinish(values: GetWeatherConditionDescriptionQuery): void;
}) {
  return (
    <Container>
      <WeatherForm onFinish={onFinish} />
    </Container>
  );
}
