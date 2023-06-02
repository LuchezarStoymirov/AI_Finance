import { fetchWrapper } from "./fetchWrapper"


const get = async (url: string) => {
    const data = await fetchWrapper.get(url);
    return data;
}

export const services = {
    get
}