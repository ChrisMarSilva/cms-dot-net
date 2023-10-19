import http from 'k6/http';
import { Trend, Rate, Counter } from 'k6/metrics';
import { check, fail, sleep } from "k6";

export let GetCustomerDuration = new Trend('get_one_customer_duration');
export let GetCustomerSuccessRate = new Rate('get_one_customer_success_rate');
export let GetCustomerFailRate = new Rate('get_one_customer_fail_rate');
export let GetCustomerReqs = new Counter('get_one_customer_reqs');

export default function () {
    let response = http.get('https://localhost:44315/customer/1');

    GetCustomerDuration.add(response.timings.duration);
    GetCustomerSuccessRate.add(response.status < 399);
    GetCustomerFailRate.add(response.status === 0 || response.status > 399);
    GetCustomerReqs.add(1);

    if (!check(response, {
        'is statuscode 200 - endpoint get customer': (r) => r.status === 200,
        'max duration': (r) => r.timings.duration < 1000,
    })) {
        fail('Falha ao verificar o status code e duration do endpoint'); // Falha na execução do cenário de teste get customer
    }

    sleep(1);
}
