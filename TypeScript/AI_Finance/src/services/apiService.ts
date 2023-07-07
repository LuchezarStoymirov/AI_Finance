import { fetchWrapper } from "./fetchWrapper";
import { config } from "../Config/urlConfig";

const authHeader = () => {
  const token = localStorage.getItem("token");
  return {
    headers: {
      Authorization: `${token}`,
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

const getUserData = async () => {
  const url = config.baseURL + config.user;
  const res = await fetchWrapper.get(url, authHeader());
  return res;
};

const logOut = async () => {
  const url = config.baseURL + config.logout;
  const res = await fetchWrapper.post(url, authHeader());
  return res;
}

const changeUsername = async () => {
  const url = config.baseURL + config.updateUser;
  const res = await fetchWrapper.post(url, authHeader());
  return res;
}

const changeEmail = async () => {
  const url = config.baseURL + config.updateUser;
  const res = await fetchWrapper.post(url, authHeader());
  return res;
}

export const apiService = {
  getStockData,
  exportData,
  getUserData,
  logOut,
  changeUsername,
  changeEmail
};
