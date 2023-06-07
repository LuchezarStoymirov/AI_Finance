import { fetchWrapper } from "./fetchWrapper"

interface Data {
  email: string;
  password: string;
}

const login = async (url: string, data: Data) => {
    const response = await fetchWrapper.post(url, data)
    return response;
}

const register = (url: string, data: Data) => {
    const response = fetchWrapper.post(url, data);
    return response;
  };
  

export const autservice = { 
    login,
    register
}
