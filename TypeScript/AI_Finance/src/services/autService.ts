import { fetchWrapper } from "./fetchWrapper"

const login = async (url: string, data: object) => {
  const config = {
    'Content-type': 'application/json'
  };
    const response = await fetchWrapper.post(url, data, config)
    // console.log('in autservice: ');
    // console.log(response);
    return response;
}

const register = (url: string, data: object) => {
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
