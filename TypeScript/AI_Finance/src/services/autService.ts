import { fetchWrapper } from "./fetchWrapper"

const login = (url: string, data: object) => {
    const config = {
        'content-type': 'application/json'
    }
    const response = fetchWrapper.post(url, data, config)
    console.log('post reached autsevice');
    return response;
}

export const autservie = { 
    login
}
