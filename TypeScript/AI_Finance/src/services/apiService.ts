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
  const url = config.baseURL + config.scraping;
  const res = await fetchWrapper.get(url, authHeader());
  return res;
};

const exportData = async () => {
  const url = config.baseURL + config.export;
  const res = await fetchWrapper.get(url, authHeader());
  return res;
};

export const apiService = {
  getStockData,
  exportData,
};
