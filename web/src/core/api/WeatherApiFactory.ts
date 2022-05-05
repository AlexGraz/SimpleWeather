import axios, { AxiosResponse } from "axios";

export type ErrorResult = {
  error: {
    message: string;
  };
};

export type SuccessResult<T> = T & { error: never };

export type Result<T> = SuccessResult<T> | ErrorResult;

export interface WeatherCondition {
  description: string;
}

export interface GetWeatherConditionDescriptionQuery {
  cityName: string;
  countryName: string;
}

function GetWeatherCondition(
  payload: GetWeatherConditionDescriptionQuery
): Promise<AxiosResponse<Result<WeatherCondition>>> {
  return axios.get("api/v1/weather", {
    params: payload,
  });
}

export const WeatherApiFactory = {
  GetWeatherCondition,
} as const;
