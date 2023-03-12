import http from 'k6/http';

// k6 run http_02_post_carga.js

export const options = {
    vus: 10,
    duration: '60s',
    thresholds: {
        http_req_failed: ['rate<0.01'],   // http errors should be less than 1%
        http_req_duration: ['p(95)<200'], // 95% of requests should be below 200ms
    },
};

export default function () {
    const url = 'https://localhost:7075/api/v1/tarefas';
    const payload = JSON.stringify({ Atividade: 'Tarefa 0001', Status: 'A' });
    const params = { headers: { 'Content-Type': 'application/json' } };
    http.post(url, payload, params);
}

