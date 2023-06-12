import { fetchWrapper } from "./fetchWrapper"
import { config } from "../Config/urlConfig";

const get = async () => {
  const url = config.baseURL;
  const token = localStorage.getItem('token');
  const header = {
    headers: {
      Authorization: `Bearer ${token}`
    }
  };
    const data = await fetchWrapper.get(url, header);
    return data;
}

export const services = {
    get
}