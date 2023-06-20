import axios, { AxiosRequestConfig } from "axios";

const get = async (url: string, config: AxiosRequestConfig<any>) => {
  const response = await axios.get(url, config);
  return response.data;
};

const post = <T, D>(url: string, data: D) => {
  return axios.post<T>(url, data);
};

export const fetchWrapper = {
  get,
  post,
};
