import { WeatherCondition } from "code-gen/api-definitions";
import styled from "styled-components";
import { Typography } from "antd";
import { WeatherVisual } from "core/components/WeatherVisual";

const { Title } = Typography;

const Container = styled.div`
  display: flex;
  justify-content: center;
  width: 100%;
  background-color: #161616;
  flex-grow: 1;
`;

const InnerContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  background-color: #161616;
  text-transform: capitalize;
`;

export function WeatherBody({
  weatherCondition,
}: {
  weatherCondition: WeatherCondition | undefined;
}) {
  return (
    <Container>
      <InnerContainer>
        <WeatherVisual weatherCondition={weatherCondition} />
        <Title style={{ color: "white" }}>
          {weatherCondition?.description}
        </Title>
      </InnerContainer>
    </Container>
  );
}
