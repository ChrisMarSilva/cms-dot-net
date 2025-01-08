import http from 'k6/http';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

// k6 run 01_message.js
// k6 run--vus 20 --duration 60s 01_message.js
// k6 run--vus 1000 --duration 600s 01_message.js
// k6 run--vus 1000 --duration 3600s 01_message.js
// k6 run--vus 20 --iterations = 100 01_message.js

// (vus) = usuários virtuais simultâneos
// (duration) = duração em milissegundos

export default function () {
    const url = `https://localhost:7222/api/rabbitmq/send`;
    const headers = { headers: { 'Content-Type': 'application/json' } };

    const payload = { text: randomString(10, `aeioubcdfghijpqrstuv`) };
    //console.log(payload);

    const res = http.post(url, JSON.stringify(payload), headers);
    //console.log(res.body);
}
