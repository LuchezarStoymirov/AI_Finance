import { AxiosResponse } from "axios";
import { fetchWrapper } from "./fetchWrapper"

interface Data {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
  email: string;
  name: string
}

interface RegisterResponse {
  name: string;
  email: string;
  password: string
}

const login = async (url: string, data: Data) : Promise<AxiosResponse<LoginResponse>> => {
    const response = await fetchWrapper.post<LoginResponse>(url, data)
    return response;
}

const register = (url: string, data: Data) => {
    const response = fetchWrapper.post<RegisterResponse>(url, data);
    return response;
  };
  

export const autservice = { 
    login,
    register
}
