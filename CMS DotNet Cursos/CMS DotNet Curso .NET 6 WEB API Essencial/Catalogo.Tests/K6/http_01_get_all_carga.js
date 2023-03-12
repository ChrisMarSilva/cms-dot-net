import http from 'k6/http';

// k6 run http_01_get_all_carga.js

export const options = {
    vus: 10,          // 100
    duration: '10s',  // 60s // 2m
    // iterations: 10, // 100
    // linger: true,
    // maxRedirects: 10,
    // minIterationDuration: '10s',
    // noConnectionReuse: true,
    // noCookiesReset: true,
    // noVUConnectionReuse: true,
    // paused: true,
    // rps: 500,
    // setupTimeout: '30s', 
    thresholds: { // Limites são os critérios de aprovação/reprovação que você define para suas métricas de teste.
        // http errors should be less than 1%
        // erros de http devem ser menores que 1%
        http_req_failed: ['rate<0.01'],   
        // 90% of requests should be below 200ms
        // 90% das requisições devem ser abaixo de 200ms
        http_req_duration: ['p(90)<200'], 
    },
};

export default function () {
    const url = 'https://localhost:7176/api/v1/categorias';
    const res = http.get(url);
}

// vus
// Current number of active virtual users
// Número atual de usuários virtuais ativos

// duration
// duration: equals to http_req_duration.
// Tempo total para a solicitação.

// data_received
// The amount of received data. This example covers how to track data for an individual URL.
// A quantidade de dados recebidos. Este exemplo aborda como controlar dados de uma URL individual.

// data_sent
// The amount of data sent. Track data for an individual URL to track data for an individual URL.
// A quantidade de dados enviados. Rastreie dados de um URL individual para rastrear dados de um URL individual.

// http_req_blocked
// Time spent blocked (waiting for a free TCP connection slot) before initiating the request.
// Tempo gasto bloqueado (aguardando um slot de conexão TCP livre) antes de iniciar a solicitação.

// http_req_connecting
// Time spent establishing TCP connection to the remote host.
// Tempo gasto estabelecendo conexão TCP com o host remoto.

// http_req_duration
// Total time for the request.
// Tempo total para a solicitação.

// http_req_failed
// The rate of failed requests according to setResponseCallback.
// A taxa de solicitações com falha de acordo com setResponseCallback.

// http_req_receiving
// Time spent receiving response data from the remote host
// Tempo gasto recebendo dados de resposta do host remoto

// http_req_sending
// Time spent sending data to the remote host
// Tempo gasto enviando dados para o host remoto

// http_req_tls_handshaking
// Time spent handshaking TLS session with remote host
// Tempo gasto apertando a mão da sessão TLS com o host remoto

// http_req_waiting
// Time spent waiting for response from remote host (a.k.a. “time to first byte”, or “TTFB”).
// Tempo gasto aguardando a resposta do host remoto (também conhecido como "tempo até o primeiro byte" ou "TTFB").

// http_reqs
// How many total HTTP requests k6 generated.
// Quantas solicitações HTTP totais k6 geraram.

// iteration_duration
// The time it took to complete one full iteration, including time spent in setup and teardown. To calculate the duration of the iteration's function for the specific scenario, try this workaround
// O tempo que levou para concluir uma iteração completa, incluindo o tempo gasto na configuração e desmontagem. Para calcular a duração da função da iteração para o cenário específico, tente esta solução alternativa

// iterations
// The aggregate number of times the VUs executed the JS script (the default function).
// O número agregado de vezes que as VUs executaram o script JS (a função padrão).
