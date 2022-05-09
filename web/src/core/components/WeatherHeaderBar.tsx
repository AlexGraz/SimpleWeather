import styled from "styled-components";
import { WeatherForm } from "core/components/WeatherForm";
import React from "react";
import { GetWeatherConditionDescriptionQuery } from "code-gen/api-definitions";

const Container = styled.div`
  width: 100%;
  display: flex;
  background-color: white;
  padding: 2rem;
  height: 6.5rem;
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
