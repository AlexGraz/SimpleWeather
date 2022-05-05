import { Form, Input } from "antd";
import React from "react";
import styled from "styled-components";
import { WeatherForm } from "core/components/WeatherForm";
import { WeatherResultCard } from "core/components/WeatherResultCard";

export function WeatherPage() {
  return (
    <div>
      <WeatherResultCard />
      <WeatherForm />
    </div>
  );
}
