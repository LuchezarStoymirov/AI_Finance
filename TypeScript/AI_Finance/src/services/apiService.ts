import { fetchWrapper } from "./fetchWrapper";
import { config } from "../Config/urlConfig";

const authHeader = () => {
  const token = localStorage.getItem("token");
  return {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };
};

const getStockData = async () => {
  const url = config.baseURL;
  const data = await fetchWrapper.get(url, authHeader());
  return data;
};

export const apiService = {
  getStockData,
};
