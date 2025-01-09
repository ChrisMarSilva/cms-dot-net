import http from 'k6/http';
import { check, sleep } from 'k6';
import { uuidv4 } from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

// winget install k6
// k6 run Cache.Api.js

// sem cache
// http_req_duration..............: avg=1.94s    min=999.3µs med=67.98ms max=6.95s    p(90)=6.88s    p(95)=6.89s
// http_req_duration..............: avg=899.93ms min=0s      med=219.68ms max=4.52s    p(90)=3.93s   p(95)=4.15s

// com cache
// http_req_duration..............: avg=67.11ms  min=1ms     med=54.64ms  max=468.34ms p(90)=129.4ms  p(95)=162.56ms
// http_req_duration..............: avg=40.57ms  min=0s      med=35.34ms max=329.78ms p(90)=74.29ms  p(95)=86.74ms
// http_req_duration..............: avg=4.65ms   min=0s med=2.52ms max=249.95ms p(90)=8.43ms   p(95)=13.73ms

export const options = {
    //// Test
    //vus: 50, // Número de usuários simultâneos
    //duration: '10s', // Duração do teste

    //// Load Test
    //// O objetivo é verificar como o sistema se comporta sob uma carga esperada (realista).
    //// 30s // 5m
    stages: [
        { duration: '1m', target: 50 },  // Sobe para 50 usuários em 1 minuto
        { duration: '5m', target: 50 },  // Mantém 50 usuários por 5 minutos
        { duration: '1m', target: 0 },   // Reduz para 0 usuários em 1 minuto
    ],

    //// Stress Test
    //// O objetivo é identificar o ponto de quebra do sistema, aumentando gradualmente a carga além do esperado.
    //stages: [
    //    { duration: '1m', target: 200 }, // Sobe para 200 usuários em 1 minuto
    //    { duration: '5m', target: 200 }, // Mantém 200 usuários por 5 minutos
    //    { duration: '1m', target: 800 }, // Sobe para 800 usuários em 1 minuto
    //    { duration: '5m', target: 800 }, // Mantém 800 usuários por 5 minutos
    //    { duration: '1m', target: 1000 }, // Sobe para 1000 usuários em 1 minuto
    //    { duration: '5m', target: 1000 }, // Mantém 1000 usuários por 5 minutos
    //    { duration: '5m', target: 0 }, // Reduz para 0 usuários em 5 minutos
    //],

    //// Soak Test
    //// O objetivo é avaliar a estabilidade do sistema sob carga sustentada durante um longo período.
    //stages: [
    //    { duration: '5m', target: 200 },  // Sobe para 200 usuários em 5 minutos
    //    { duration: '30m', target: 200 }, // Mantém 200 usuários por 30 minutos
    //    { duration: '5m', target: 0 },    // Reduz para 0 usuários em 5 minutos
    //],

    //// Spike Test
    //// Avaliar a capacidade do sistema de lidar com picos repentinos de carga.
    //stages: [
    //    { duration: '1m', target: 50 },   // Sobe para 50 usuários em 1 minuto
    //    { duration: '30s', target: 300 }, // Pico súbito de 300 usuários
    //    { duration: '1m', target: 50 },   // Retorna a 50 usuários
    //],

    //// Capacity Test
    //// Determinar a capacidade máxima do sistema antes de apresentar falhas.
    //stages: [
    //    { duration: '2m', target: 100 }, // Sobe para 100 usuários em 2 minutos
    //    { duration: '2m', target: 200 }, // Sobe para 200 usuários em 2 minutos
    //    { duration: '2m', target: 300 }, // Sobe para 300 usuários
    // Continua até encontrar o limite...
    //],

    //// Endurance Test
    //// Similar ao Soak Test, mas com foco na resistência ao longo do tempo para detectar degradação de desempenho.
    //stages: [
    //    { duration: '10m', target: 50 },  // Sobe para 50 usuários
    //    { duration: '2h', target: 50 },  // Mantém 50 usuários por 2 horas
    //    { duration: '10m', target: 0 },  // Reduz para 0 usuários
    //],

    //// Scalability Test: Avaliar como o sistema se comporta ao escalar horizontalmente ou verticalmente (por exemplo, aumentando servidores ou recursos).
    //// Resilience Test: Testar  a capacidade do sistema de continuar funcionando diante de falhas inesperadas, como interrupções de serviços ou perda de conexão.
    //// Latency Test: Medir os tempos de resposta do sistema sob diferentes condições de carga.
    //// Configuration Test: Avaliar o impacto de diferentes configurações do sistema no desempenho.
    //// Failover Test: Garantir que o sistema é capaz de redirecionar ou recuperar serviços durante falhas.
    //// Concurrency Test: Avaliar como o sistema lida com várias transações ou threads simultâneas.
    //// Break Point Test: Descobrir o ponto exato onde o sistema falha ao aumentar a carga gradativamente.

    thresholds: {
        http_req_failed: ['rate<0.01'], // http errors should be less than 1% // During the whole test execution, the error rate must be lower than 1%.
        //http_req_duration: ['p(95)<200'], // 95% of requests should be below 200ms
        http_req_duration: ['p(90)<400'], // 90% of requests must finish within 400ms.
        //http_req_duration: ['p(90) < 400', 'p(95) < 800', 'p(99.9) < 2000'], // 90% of requests must finish within 400ms, 95% within 800, and 99.9% within 2s.
        //http_req_duration: ['avg<100', 'p(95)<200']
    },

    noConnectionReuse: true,
    //userAgent: 'MyK6UserAgentString/1.0',
    //insecureSkipTLSVerify: true,

    //    scenarios: {
    //        max_load_test: {
    //            executor: 'constant-arrival-rate',
    //            rate: 30000, // Number of requests per second
    //            timeUnit: '1s', // The time unit over which 'rate' is defined
    //            duration: '1m', // Test duration (1 minutes)
    //            preAllocatedVUs: 20, // Preallocate 200 Virtual Users
    //            maxVUs: 100, // Allow up to 100 Virtual Users
    //        },
    //    },
};

export default function () {

    const id = "deb004d2-14fd-430a-b919-aa104a20552f";
    const url = `http://localhost:5042/api/v1/user/${id}`;
    const headers = {
        headers: {
            'Content-Type': 'application/json',
            'Idempotency-Ke': uuidv4(),
        }
    };

    //const payload = { text: randomString(10, `aeioubcdfghijpqrstuv`) };
    //console.log(payload);

    //const res = http.post(url, JSON.stringify(payload), headers);
    const res = http.get(url, { headers });
    //console.log(`Status: ${res.status}, Body: ${res.body}`);

    check(res, {
        'status is 200': (r) => r.status === 200,
        //'status is 500': (r) => r.status == 500,
        //'body is not empty': (r) => r.body.length > 0,
        //'body size is 11,105 bytes': (r) => r.body.length == 11105,
    });

    //if (res.status !== 200) {
    //    console.error(`Erro: Status ${res.status}, Body: ${res.body}`);
    //}

    sleep(1); // seconds
}