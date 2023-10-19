import http from 'k6/http';
import { Trend, Rate, Counter } from 'k6/metrics';
import { check, fail, sleep } from "k6";

export let GetAllCustomerDuration = new Trend('get_all_customer_duration');
export let GetAllCustomerSuccessRate = new Rate('get_all_customer_success_rate');
export let GetAllCustomerFailRate = new Rate('get_all_customer_fail_rate');
export let GetAllCustomerReqs = new Counter('get_all_customer_reqs');

export default function () {
    let response = http.get('https://localhost:44315/api/Customers');

    GetAllCustomerDuration.add(response.timings.duration);
    GetAllCustomerSuccessRate.add(response.status < 399);
    GetAllCustomerFailRate.add(response.status === 0 || response.status > 399);
    GetAllCustomerReqs.add(1);

    if (!check(response, {
        'is statuscode 200 - endpoint get all customer': (r) => r.status === 200,
        'max duration': (r) => r.timings.duration < 1000,
    })) {
        fail('Falha ao verificar o status code e duration do endpoint'); // Falha na execução do cenário de teste get all customer
    }

    sleep(1);
}
