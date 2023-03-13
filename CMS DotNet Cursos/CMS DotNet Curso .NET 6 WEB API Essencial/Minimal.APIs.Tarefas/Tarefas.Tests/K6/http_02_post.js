import http from 'k6/http';

// k6 run http_02_post.js

export default function () {
    const url = 'https://localhost:7075/api/v1/tarefas';
    const payload = JSON.stringify({ Atividade: 'Tarefa 0001', Status: 'A'});
    const params = { headers: { 'Content-Type': 'application/json' } };
    http.post(url, payload, params);
}

