import http from 'k6/http';

// k6 run http_01_get_all.js
// https://k6.io/docs/using-k6/http-requests/

export default function () {
    const url = 'http://localhost:5207/api/v1/dev-events';
    const res = http.get(url);

    //check(res, {
    //    'status is 500': (r) => r.status == 500,
    //});

    //sleep(1);

//    for (let id = 1; id <= 100; id++) {
//        http.get(`http://example.com/posts/${id}`);
//    }

//    for (let id = 1; id <= 100; id++) {
//        http.get(`http://example.com/posts/${id}`, {
//            tags: { name: 'PostsItemURL' },
//        });
//    }

    //for (let id = 1; id <= 100; id++) {
    //    http.get(http.url`http://example.com/posts/${id}`);
    //}

}