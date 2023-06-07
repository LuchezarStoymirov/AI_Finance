import axios from "axios";

interface Data {
  email: string,
  password: string;
}

const get = async (url: string) => {
    const response = await axios.get(url);
    return response.data;
};

const post = async (url:string, data: Data) => {
  let response;
  await axios.post(url, data).then(res => {
    response = res.data;
  })
  return response;
}

export const fetchWrapper = {
  get,
  post
};
