import axios from "axios";

interface Data {
  email: string,
  password: string;
}

const get = async (url: string) => {
    const headers = {
      'Content-type' : 'application/json',
      'Authorization' : `Bearer ${localStorage.getItem('token')}`
    }
    const response = await axios.get(url, {headers});
    return response.data;
};

const post = <T,>(url:string, data: Data) => {
  return axios.post<T>(url, data);
}

export const fetchWrapper = {
  get,
  post
};
