import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 100 },  // 100 VUs por 10s → ~1000 reqs
        { duration: '5s', target: 400 },   // 400 VUs por 5s → ~2000 reqs
        { duration: '30s', target: 167 },  // 167 VUs por 30s → ~5000 reqs
    ],
};

export default function () {
    http.get('http://localhost:5121/Emprestimo/1');
    sleep(1);
}


//increase(process_cpu_time_seconds_total{app="BFF",env="Development"}[$__rate_interval])

//increase(requisicoes_total{app="BFF"}[$__rate_interval])

//histogram_quantile(0.5, sum(rate(http_server_request_duration_seconds_bucket{app="BFF"}[$__rate_interval])) by (le))
//histogram_quantile(0.95, sum(rate(http_server_request_duration_seconds_bucket{app="BFF"}[$__rate_interval])) by (le))
//histogram_quantile(0.90, sum(rate(http_server_request_duration_seconds_bucket{app="BFF"}[$__rate_interval])) by (le))
//rate(requisicoes_total{app="BFF",tipo="http_sucesso"}[$__rate_interval])