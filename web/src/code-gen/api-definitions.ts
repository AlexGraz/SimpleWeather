// CodeGen - do not edit or delete
// eslint-disable
export interface GetRequest<TResponse> {
  url: string;
  method: "GET";
}
export interface OptionsRequest<TResponse> {
  url: string;
  method: "OPTIONS";
}
export interface DeleteRequest<TResponse> {
  url: string;
  method: "DELETE";
}
export interface PostRequest<TRequest, TResponse> {
  data: TRequest;
  url: string;
  method: "POST";
}
export interface PatchRequest<TRequest, TResponse> {
  data: TRequest;
  url: string;
  method: "PATCH";
}
export interface PutRequest<TRequest, TResponse> {
  data: TRequest;
  url: string;
  method: "PUT";
}
export const toQuery = (o: { [key: string]: any }): string => {
  const q = Object.keys(o)
    .map((k) => ({ k, v: o[k] }))
    .filter((x) => x.v !== undefined && x.v !== null)
    .map((x) =>
      Array.isArray(x.v)
        ? x.v
            .map((v) => `${encodeURIComponent(x.k)}=${encodeURIComponent(v)}`)
            .join("&")
        : `${encodeURIComponent(x.k)}=${encodeURIComponent(x.v)}`
    )
    .join("&");
  return q ? `?${q}` : "";
};
export abstract class WeatherRequestFactory {
  static GetWeatherConditionDescription(
    conditionDescriptionQuery: GetWeatherConditionDescriptionQuery
  ): GetRequest<Result<WeatherCondition>> {
    const query = toQuery({ ...conditionDescriptionQuery });
    return {
      method: "GET",
      url: `/api/v1/weather${query}`,
    };
  }
}
export interface FailResult<TSuccess> {
  isSuccessful: false;
  message: string;
  statusCode?: number;
}
export interface GetWeatherConditionDescriptionQuery {
  cityName: string;
  countryName: string;
}
export type Result<TSuccess> = SuccessResult<TSuccess> | FailResult<TSuccess>;
export interface SuccessResult<TSuccess> {
  isSuccessful: true;
  data: TSuccess;
  statusCode?: number;
}
export interface WeatherCondition {
  description: string;
}
