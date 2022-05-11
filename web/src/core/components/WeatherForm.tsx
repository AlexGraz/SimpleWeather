import { Button, Form, Input } from "antd";
import React from "react";
import { GetWeatherConditionDescriptionQuery } from "code-gen/api-definitions";
import styled from "styled-components";

const FormItemContainer = styled.div`
  display: flex;
  gap: 1rem;
`;

export function WeatherForm({
  onSubmit,
}: {
  onSubmit(values: GetWeatherConditionDescriptionQuery): void;
}) {
  const itemStyle = { margin: 0, width: "20rem" };

  return (
    <Form onFinish={onSubmit} layout={"inline"}>
      <FormItemContainer>
        <Form.Item
          name={"CityName"}
          label={"City"}
          rules={[{ required: true, message: "City name is required" }]}
          style={itemStyle}
        >
          <Input placeholder={"Name"} />
        </Form.Item>
        <Form.Item
          name={"CountryName"}
          label={"Country"}
          rules={[{ required: true, message: "Country name is required" }]}
          style={itemStyle}
        >
          <Input placeholder={"Name"} />
        </Form.Item>
        <Button htmlType={"submit"}>Submit</Button>
      </FormItemContainer>
    </Form>
  );
}
