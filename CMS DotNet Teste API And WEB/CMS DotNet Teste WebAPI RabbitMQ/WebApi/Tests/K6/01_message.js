import http from 'k6/http';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export default function () {
    const url = `https://localhost:7222/api/rabbitmq/send`;
    const headers = { headers: { 'Content-Type': 'application/json' } };

    const payload = { text: randomString(10, `aeioubcdfghijpqrstuv`) };
    //console.log(payload);

    const res = http.post(url, JSON.stringify(payload), headers);
    //console.log(res.body);
}
