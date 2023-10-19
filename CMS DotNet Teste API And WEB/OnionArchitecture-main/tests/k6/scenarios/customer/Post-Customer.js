import http from 'k6/http';
import { Trend, Rate, Counter } from 'k6/metrics';
import { check, fail, sleep } from "k6";

export let PostCustomerDuration = new Trend('post_customer_duration');
export let PostCustomerSuccessRate = new Rate('post_customer_success_rate');
export let PostCustomerFailRate = new Rate('post_customer_fail_rate');
export let PostCustomerReqs = new Counter('post_customer_reqs');

export default function () {

    let payload = JSON.stringify({
        "createDate": "2023-10-19T14:31:47.935Z",
        "modifiedDate": "2023-10-19T14:31:47.935Z",
        "isActive": true,
        "customerName": "Chris K6",
        "purchasesProduct": "Curso K6",
        "paymentType": "Boleto"
    });

    let params = { headers: { 'Content-Type': 'application/json' } };
    let url = 'https://localhost:44315/customer';

    let response = http.post(url, payload, params);

    PostCustomerDuration.add(response.timings.duration);
    PostCustomerSuccessRate.add(response.status < 399);
    PostCustomerFailRate.add(response.status === 0 || response.status > 399);
    PostCustomerReqs.add(1);

    if (!check(response, {
        'is statuscode 200 - endpoint post customer': (r) => r.status === 200,
        'max duration': (r) => r.timings.duration < 1000,
    })) {
        fail('Falha ao verificar o status code e duration do endpoint'); // Falha na execução do cenário de teste post customer
    }

    sleep(1);
}
