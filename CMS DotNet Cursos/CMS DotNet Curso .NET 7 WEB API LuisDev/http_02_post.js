import http from 'k6/http';

// k6 run http_02_post.js

export default function () {
    const url = 'http://localhost:5207/api/v1/dev-events';
    const payload = JSON.stringify({ title: 'Pessoa 1', description: 'Descricao Pessoa 1'});
    const params = {headers: {'Content-Type': 'application/json'}};
    http.post(url, payload, params);
}

