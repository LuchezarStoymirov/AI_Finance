import { fetchWrapper } from "./fetchWrapper"

const login = (url: string, data: object) => {
  const config = {
    'Content-type': 'application/json'
  };
    const response = fetchWrapper.post(url, data, config)
    console.log('post reached autsevice');
    return response;
}

const register = async (url: string, data: object) => {
    const config = {
      'Content-type': 'application/json'
    };
    const response = fetchWrapper.post(url, data, config);
    console.log('post reached autservice');
    return response;
  };
  

export const autservice = { 
    login,
    register
}
