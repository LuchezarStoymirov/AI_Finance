import { AxiosResponse } from "axios";
import { fetchWrapper } from "./fetchWrapper"
import { config } from "../Config/urlConfig";

interface LoginData {
  email: string;
  password: string;
}

interface RegisterData {
  name: string;
  email: string;
  password: string
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

const login = async (data: LoginData) : Promise<AxiosResponse<LoginResponse>> => {
    const url = config.baseURL + config.login;
    const response = await fetchWrapper.post<LoginResponse, LoginData>(url, data)
    return response;
}

const register = (data: RegisterData) => {
    const url = config.baseURL + config.register;
    const response = fetchWrapper.post<RegisterResponse, RegisterData>(url, data);
    return response;
  };

export const autservice = { 
    login,
    register
}
