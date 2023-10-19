import { group, sleep } from 'k6';
import GetOneCustomer from "./scenarios/customer/Get-Customer.js";
import GetAllCustomer from "./scenarios/customer/GetAll-Customer.js";
import PostCustomer from "./scenarios/customer/Post-Customer.js";
// import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

// export function handleSummary(data) {
//     return {
//         "summary.html": htmlReport(data),
//     };
// }

// k6 run index.js
// k6 run index.js --vus 20 --duration 10s
// k6 run index.js --vus 200 --duration 10s

// k6 login cloud --token xxxxx
// k6 run --out cloud index.js 
// k6 run --out cloud index.js --vus 20 --duration 10s
// k6 run --out cloud index.js --vus 30 --duration 6s

// k6 run --out influxdb=http://localhost:8086/k6 index.js --vus 20 --duration 10s
// k6 run --out influxdb=http://localhost:8086/k6 index.js --vus 1000 --duration 1m 

// k6 run --out csv=ResultadosK6.csv index.js
// k6 run --out json=ResultadosK6.json index.js

export default () => {
    group('API k6 - Endpoint Get One Customer', () => {
        GetOneCustomer();
        sleep(1);
    });

    group('API k6 - Endpoint Get All Customer', () => {
        GetAllCustomer();
        sleep(1);
    });

    group('API k6 - Endpoint Post Customer', () => {
        PostCustomer();
        sleep(1);
    });
}

