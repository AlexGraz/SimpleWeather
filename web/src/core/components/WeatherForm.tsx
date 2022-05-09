import { Button, Form, Input } from "antd";
import React from "react";
import { GetWeatherConditionDescriptionQuery } from "code-gen/api-definitions";

export function WeatherForm({
  onFinish,
}: {
  onFinish(values: GetWeatherConditionDescriptionQuery): void;
}) {
  return (
    <>
      <Form onFinish={onFinish} layout={"inline"}>
        <Form.Item name={"CityName"} label={"City"}>
          <Input placeholder={"Name"} />
        </Form.Item>
        <Form.Item name={"CountryName"} label={"Country"}>
          <Input placeholder={"Name"} />
        </Form.Item>
        <Button htmlType={"submit"}>Submit</Button>
      </Form>
    </>
  );
}
