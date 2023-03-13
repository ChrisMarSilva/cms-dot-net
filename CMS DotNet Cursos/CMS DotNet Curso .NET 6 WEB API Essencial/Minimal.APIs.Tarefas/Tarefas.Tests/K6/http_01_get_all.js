import http from 'k6/http';

// k6 run http_01_get_all.js
// https://k6.io/docs/using-k6/http-requests/

export default function () {
    const url = 'https://localhost:7075/api/v1/tarefas';
    const res = http.get(url);
}
