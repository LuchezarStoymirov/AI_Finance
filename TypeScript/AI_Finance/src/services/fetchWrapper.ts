import axios from "axios";

const get = async (url: string) => {
  
    const response = await axios.get(url);
    return response.data;
};

const post = async (url:string, data: object, config: object) => {
  let response;
  await axios.post(url, data, config).then(res => {
    response = res.data;
  })
  // console.log('in wrapper: ');
  // console.log(response);
  return response;
}

export const fetchWrapper = {
  get,
  post
};
