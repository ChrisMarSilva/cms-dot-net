import http from 'k6/http';

// k6 run http_02_post.js

export default function () {
    const url = 'https://localhost:7176/api/v1/categorias';
    const payload = JSON.stringify({ Nome: 'Categoria 0001', ImagemUrl: 'ImagemCategoria0001'});
    const params = { headers: { 'Content-Type': 'application/json' } };
    http.post(url, payload, params);
}

