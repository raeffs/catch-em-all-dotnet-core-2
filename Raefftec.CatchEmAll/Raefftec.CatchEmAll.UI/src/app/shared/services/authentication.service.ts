import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthenticationService {

    private token: string;

    constructor(
        private http: HttpClient
    ) { }

    public isAuthenticated(): boolean {
        return !!this.token;
    }

    public login(username: string, password: string): Observable<boolean> {
        return Observable.of(!!username && username === password)
            .map(success => {
                this.token = success ? 'tokenvalue' : '';
                return success;
            });

        /*
        return this.http.post<string>('http://localhost:25084/api/token', null, { params: { username, password } })
            .map(token => {
                this.token = token;
            });
            */
    }

}
