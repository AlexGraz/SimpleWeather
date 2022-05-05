import { useCallback, useMemo, useState } from "react";
import { Result, SuccessResult } from "core/api/WeatherApiFactory";
import { AxiosResponse } from "axios";

export function useWeatherApiAsync<T>(
  api: () => Promise<AxiosResponse<Result<T>>>
) {
  const [response, setResponse] = useState<AxiosResponse<Result<T>>>();
  const [loading, setLoading] = useState<boolean>(false);

  const getData = useCallback(async () => {
    setLoading(true);
    setResponse(await api());
    setLoading(false);
  }, [api]);

  if (!response && !loading) {
    void getData();
  }

  return useMemo(
    () => ({
      data:
        response && isSuccessful(response.data) ? response?.data : undefined,
      error:
        response && !isSuccessful(response.data)
          ? response?.data.error.message
          : undefined,
      loading,
      refreshData: getData,
    }),
    [getData, loading, response]
  );
}

function isSuccessful<T>(result: Result<T>): result is SuccessResult<T> {
  return !Object.prototype.hasOwnProperty.call(result, "error");
}
