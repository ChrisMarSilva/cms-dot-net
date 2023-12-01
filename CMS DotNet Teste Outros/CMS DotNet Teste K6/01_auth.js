import http from 'k6/http';

export function authenticateJDOAuth() { // export default function () {

    const url = 'http://localhost:60001/jd/oauth/connect/token?api-version=1';

    var payload = {
        client_id: 'admin',
        client_secret: 'JD@dmin2020',
        grant_type: 'client_credentials',
        scope: 'oauth_api',
    }

    const headers = {
        'User-Agent': ' Mozilla/5.0 (Macintosh; Intel Mac OS X 10.9; rv:48.0) Gecko/20100101 Firefox/48.0',
        'Accept': ' text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
        'Accept-Language': ' fr,en-US;q=0.7,en;q=0.3',
        'Accept-Encoding': 'gzip, deflate',
        'Content-Type': 'application/x-www-form-urlencoded'
    };

    const res = http.post(url, payload, headers);
    //console.log(res.body)
    //console.log(res.json('access_token'))

    return res.json()
}
