import React from "react";
import styled from "styled-components";
import { WeatherPage } from "core/WeatherPage";

const Container = styled.main`
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
`;

export function App() {
  return (
    <Container>
      <WeatherPage />
    </Container>
  );
}
