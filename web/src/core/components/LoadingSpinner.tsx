import React from "react";
import { Spin } from "antd";
import { LoadingOutlined } from "@ant-design/icons";
import { createGlobalStyle } from "styled-components";

const SpinStyle = createGlobalStyle`
  .ant-spin-container::after {
    background: transparent;
  }
`;

const antIcon = (
  <LoadingOutlined style={{ fontSize: 80, color: "grey" }} spin />
);

export function LoadingSpinner({
  ...props
}: React.ComponentPropsWithoutRef<typeof Spin>) {
  return (
    <>
      <SpinStyle />
      <Spin {...props} indicator={antIcon} />
    </>
  );
}
