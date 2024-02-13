import http from 'k6/http';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export default function () {

    const idCliente = randomIntBetween(1, 5);

    // const url = `http://localhost:65005/clientes/${idCliente.toString()}/extrato`; // IIS
    const url = `http://localhost:5097/clientes/${idCliente.toString()}/extrato`; // Degub 

    const params = { headers: { 'Accept-Encoding': 'gzip, deflate', 'Content-Type': 'application/json' } };

    const res = http.get(url, params);
    // console.log(res.body);
}
