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
    http.get('http://localhost:5084/Emprestimo/1');
    sleep(1);
}
