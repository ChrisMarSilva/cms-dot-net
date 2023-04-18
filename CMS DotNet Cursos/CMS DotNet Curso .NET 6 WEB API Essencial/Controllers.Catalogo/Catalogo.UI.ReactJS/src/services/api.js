import axios from 'axios';

const api = axios.create({
    baseURL: "https://localhost:7176",
})

export default api;