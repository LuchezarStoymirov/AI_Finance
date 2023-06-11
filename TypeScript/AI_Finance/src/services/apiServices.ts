import { fetchWrapper } from "./fetchWrapper"

const get = async (url: string) => {
    const token = localStorage.getItem('token');
  const config = {
    headers: {
      Authorization: `Bearer ${token}`
    }
  };
    const data = await fetchWrapper.get(url, config);
    return data;
}

export const services = {
    get
}