import http from 'k6/http';
import { uuidv4 } from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';
import { authenticateJDOAuth } from './01_auth.js';

export function setup() {
    const token = authenticateJDOAuth();
    //console.log( token.json('access_token') );
    return token; // token.json('access_token');
}

export default function (data) {

    // const url = `http://localhost:65005/jdnpc/destinatario/api/v1/titulo/23090111345234506329/detalhar?chaveIdempotenciaConsultada=${uuidv4().toString()}`; // IIS
    const url = `https://localhost:60354/jdnpc/destinatario/api/v1/titulo/23090111345234506329/detalhar?chaveIdempotenciaConsultada=${uuidv4().toString()}`; // Degub
	  
    const params = {
        headers: {
            //'User-Agent': ' Mozilla/5.0 (Macintosh; Intel Mac OS X 10.9; rv:48.0) Gecko/20100101 Firefox/48.0',
            //'Accept': ' text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
            //'Accept-Language': ' fr,en-US;q=0.7,en;q=0.3',
            'Accept-Encoding': 'gzip, deflate',
            'Content-Type': 'application/json',
            //'Chave-idempotencia': uuidv4().toString(),
            'Authorization': `Bearer ${data.access_token}`,
        },
    };

    const res = http.get(url, params);
    //console.log(res.body);
}
