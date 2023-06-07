import axios, { AxiosResponse } from "axios";

interface Data {
  email: string,
  password: string;
}

const get = async (url: string) => {
    const header = {
      'Content-type' : 'application/json',
      Authorization : {}
    }
    const response = await axios.get(url);
    return response.data;
};

const post = <T,>(url:string, data: Data) => {
  return axios.post<T>(url, data);
}

export const fetchWrapper = {
  get,
  post
};
