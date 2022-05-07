import { Button, Form, Input } from "antd";
import React from "react";
import { Api } from "core/api/Api";
import { useWeatherApiRequester } from "core/hooks/useWeatherApiRequester";

export function WeatherForm() {
  const { data, getData } = useWeatherApiRequester(
    Api.Weather.GetWeatherConditionDescription
  );

  return (
    <>
      <div>{data?.data.description}</div>
      <Form onFinish={getData}>
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
