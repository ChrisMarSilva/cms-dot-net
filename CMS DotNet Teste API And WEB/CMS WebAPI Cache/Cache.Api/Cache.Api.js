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



/*

import http from "k6/http";
import { check } from "k6";

export let options = {
  noConnectionReuse: false,
  insecureSkipTLSVerify: true,
  scenarios: {
    max_load_test: {
      executor: 'constant-arrival-rate',
      rate: 30000, // Number of requests per second
      timeUnit: '1s', // The time unit over which 'rate' is defined
      duration: '1m', // Test duration (1 minutes)
      preAllocatedVUs: 20, // Preallocate 200 Virtual Users
      maxVUs: 100, // Allow up to 100 Virtual Users
    },
  },
};

export default function () {
  
  const id = "236cf9dd-7664-480e-b7f4-99f4e3790d71";
  const response = http.get(`http://localhost:5001/api/books/${id}`);

  check(response, {
    "status is 200": (r) => r.status === 200,
    "body is not empty": (r) => r.body.length > 0,
  });
}

*/