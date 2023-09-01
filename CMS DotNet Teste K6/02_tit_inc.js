import http from 'k6/http';
import { check, sleep } from "k6";
import { uuidv4 } from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';
import { authenticateJDOAuth } from './01_auth.js';

// export const options = {
//     thresholds: {
//       // Assert that 99% of requests finish within 3000ms.
//       http_req_duration: ["p(99) < 3000"],
//     },
//     // Ramp the number of virtual users up and down
//     stages: [
//       { duration: "30s", target: 15 },
//       { duration: "1m", target: 15 },
//       { duration: "20s", target: 0 },
//     ],
// };

  
export function setup() {
    const token = authenticateJDOAuth();
    //console.log( token.json('access_token') );
    return token; // token.json('access_token');
}

export default function (data) {

    // const url = 'http://localhost:65005/jdnpc/destinatario/api/v1/titulo'; // IIS
    const url = 'https://localhost:60354/jdnpc/destinatario/api/v1/titulo'; // Degub

    var payload = {
        IdReqSistemaCliente: uuidv4().toString(),
        NumCtrlPart: "100000000000000000000",
        ISPBPartDestinatarioPrincipal: 4358798,
        ISPBPartDestinatarioAdmtd: 4358798,
        CodPartDestinatario: 77,
        TpPessoaBenfcrioOr: "F",
        CpfCnpjBenfcrioOr: 35034411822,
        NomRzSocBenfcrioOr: "Razao Social BenfcrioOr",
        NomFantsBenfcrioOr: "Nome Fantasia BenfcrioOr",
        LogradBenfcrioOr: "Rua do BenfcrioOr 111",
        CidBenfcrioOr: "Cidade do BenfcrioOr 111",
        UfBenfcrioOr: "SP",
        CepBenfcrioOr: "11111111",
        TpPessoaBenfcrioFinl: "F",
        CpfCnpjBenfcrioFinl: 35034411822,
        NomRzSocBenfcrioFinl: "Razao Social BenfcrioFinl",
        NomFantsBenfcrioFinl: "Nome Fantasia BenfcrioFinl",
        TpPessoaPagdr: "J",
        CpfCnpjPagdr: 39985277484501,
        NomRzSocPagdr: "Razao Social Pagdr",
        NomFantsPagdr: "Nome Fantasia Pagdr",
        LogradPagdr: "Rua do Pagdr 333",
        CidPagdr: "Cidade do Pagdr 333",
        UfPagdr: "SP",
        CepPagdr: "33333333",
        TpIdentcSacdrAvalst: 2,
        IdentcSacdrAvalst: 47782312092690,
        NomRzSocSacdrAvalst: "Razao Social Avalst",
        CodCartTit: 1,
        CodMoedaCNAB: 9,
        IdentdNossoNum: "13245678901324567890",
        NumCodBarras: "07799707771500000100000029170110202301230001",
        NumLinhaDigtvl: "07799707771500000100000029170110202301231000001",
        DtVencTit: "2050-01-01",
        VlrTit: 100,
        NumDocTit: "132456789013245",
        CodEspTit: 31,
        DtEmsTit: "2023-08-23",
        QtdDiaPrott: 1,
        DtLimPgtoTit: "2024-03-29",
        TpPgtoTit: 1,
        NumParcl: 1,
        QtdTotParcl: 1,
        IndrTitNegcd: "N",
        IndrBloqPgto: "N",
        IndrPgtoParcl: "N",
        QtdPgtoParcl: 1,
        VlrAbattTit: 0.01,
        JurosTit: {
            DtJurosTit: "2024-07-29",
            CodJurosTit: 3,
            VlrPercJurosTit: 50.0
        },
        MultaTit: {
            DtMultaTit: "2024-06-29",
            CodMultaTit: 2,
            VlrPercMultaTit: 40.0
        },
        DesctTit: [
            {
                DtDesctTit: "2024-03-29",
                CodDesctTit: 1,
                VlrPercDesctTit: 10.0
            },
            {
                DtDesctTit: "2024-04-29",
                CodDesctTit: 1,
                VlrPercDesctTit: 20.0
            },
            {
                DtDesctTit: "2024-05-29",
                CodDesctTit: 1,
                VlrPercDesctTit: 30.0
            }
        ],
        NotaFis: [
            {
                NumNotaFis: "000001",
                DtEmsNotaFis: "2024-03-29",
                VlrNotaFis: 100.0
            },
            {
                NumNotaFis: "000002",
                DtEmsNotaFis: "2024-03-30",
                VlrNotaFis: 200.0
            }
        ],
        TpVlrPercMinTit: "P",
        VlrPercMinTit: 10.0,
        TpVlrPercMaxTit: "V",
        VlrPercMaxTit: 100.0,
        TpModlCalc: 1,
        TpAutcRecbtVlrDivgte: 1,
        Calc: [
            {
                VlrCalcdJuros: 1.0,
                VlrCalcdMulta: 2.0,
                VlrCalcdDesct: 3.0,
                VlrTotCobrar: 4.0,
                DtValiddCalc: "2024-03-29"
            },
            {
                VlrCalcdJuros: 11.0,
                VlrCalcdMulta: 22.0,
                VlrCalcdDesct: 33.0,
                VlrTotCobrar: 44.0,
                DtValiddCalc: "2024-04-29"
            }
        ],
        TxtInfBenfcrio: [
            "Texto 01",
            "Texto 02",
            "Texto 03"
        ],
        DtMovto: "2024-08-31"
    };

    const headers = {
        headers: {
            //'User-Agent': ' Mozilla/5.0 (Macintosh; Intel Mac OS X 10.9; rv:48.0) Gecko/20100101 Firefox/48.0',
            //'Accept': ' text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
            //'Accept-Language': ' fr,en-US;q=0.7,en;q=0.3',
            'Accept-Encoding': 'gzip, deflate',
            'Content-Type': 'application/json',
            'Chave-idempotencia': uuidv4().toString(),
            'Authorization': `Bearer ${data.access_token}`,
        },
    };

    const res = http.post(url, JSON.stringify(payload), headers);
    // console.log(res.body);

    //check(res, { "status was 200": (r) => r.status == 200 });
    //  sleep(1);
}
