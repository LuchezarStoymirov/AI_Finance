import { AxiosResponse } from "axios";
import { fetchWrapper } from "./fetchWrapper"
import { config } from "../Config/urlConfig";

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

const login = async (data: Data) : Promise<AxiosResponse<LoginResponse>> => {
    const url = config.baseURL + config.login;
    const response = await fetchWrapper.post<LoginResponse>(url, data)
    return response;
}

const register = (data: Data) => {
    const url = config.baseURL + config.register;
    const response = fetchWrapper.post<RegisterResponse>(url, data);
    return response;
  };
  

export const autservice = { 
    login,
    register
}
