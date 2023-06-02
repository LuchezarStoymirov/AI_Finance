import axios from "axios";

const get = async (url: string) => {
  
    const response = await axios.get(url);
    return response.data;
};

const post = async (url:string, data: object, config: object) => {
  const response = await axios.post(url, data, config)
  console.log('post reacher wrapper');
  return response;
}

export const fetchWrapper = {
  get,
  post
};
