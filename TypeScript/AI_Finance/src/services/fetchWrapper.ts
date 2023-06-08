import axios from "axios";



const get = async (url: string) => {
    const response = await axios.get(url);
    return response.data;
};

const post = <T, D>(url:string, data: D) => {
  return axios.post<T>(url, data);
}

export const fetchWrapper = {
  get,
  post
};
