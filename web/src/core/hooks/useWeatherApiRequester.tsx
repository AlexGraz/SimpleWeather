import { useCallback, useMemo, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { GetRequest, Result } from "code-gen/api-definitions";

export function useWeatherApiRequester<T, TQuery>(
  apiConfig: (conditionDescriptionQuery: TQuery) => GetRequest<Result<T>>
) {
  const [response, setResponse] = useState<AxiosResponse<Result<T>>>();
  const [loading, setLoading] = useState<boolean>(false);

  const getData = useCallback(
    async (query: TQuery) => {
      setLoading(true);

      let config = apiConfig(query);
      let response = await axios(config.url, {
        method: config.method,
        headers: {
          Authorization: `Bearer 1f2eb11d-6fa8-41de-89f7-f0b99adedba6`,
        },
      });

      setResponse(response);
      setLoading(false);
    },
    [apiConfig]
  );

  return useMemo(
    () => ({
      data: response && response.data.isSuccessful ? response?.data : undefined,
      error:
        response && !response.data.isSuccessful
          ? response?.data.message
          : undefined,
      loading,
      getData: getData,
    }),
    [getData, loading, response]
  );
}
