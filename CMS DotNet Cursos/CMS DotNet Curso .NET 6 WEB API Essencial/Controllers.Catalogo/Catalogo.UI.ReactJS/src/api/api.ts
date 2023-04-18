import axios from "axios";
import { Aluno } from "../models/AlunoModel";
const URL = "https://627fe5d97532b4920f69de5e.mockapi.io/";

async function getUsers(): Promise<Aluno[]> {
    const response = await axios.get<Aluno[]>(`${URL}users`);

    return response.data;
}
async function updateUserName(userId: string, name: string): Promise<Aluno> {
    const response = await axios.put<Aluno>(`${URL}users/${userId}`, { name });
    return response.data;
}

export const api = {
    getUsers,
    updateUserName,
};