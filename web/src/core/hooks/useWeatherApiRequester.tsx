import { useCallback, useMemo, useState } from "react";
import axios, { AxiosError, AxiosResponse } from "axios";
import { GetRequest, Result } from "code-gen/api-definitions";
import { API_KEY } from "core/api/Api";

export function useWeatherApiRequester<T, TQuery>(
  apiConfig: (conditionDescriptionQuery: TQuery) => GetRequest<Result<T>>
) {
  const [response, setResponse] = useState<AxiosResponse<Result<T>>>();
  const [loading, setLoading] = useState<boolean>(false);

  const getData = useCallback(
    async (query: TQuery) => {
      setLoading(true);

      let config = apiConfig(query);
      try {
        let response = await axios(config.url, {
          method: config.method,
          headers: {
            Authorization: `Bearer ${API_KEY}`,
          },
        });

        setResponse(response);
      } catch (error) {
        setResponse((error as AxiosError<Result<T>>).response);
      }

      setLoading(false);
    },
    [apiConfig]
  );

  return useMemo(
    () => ({
      data:
        response && response.data.isSuccessful
          ? response?.data.data
          : undefined,
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
