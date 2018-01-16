import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { TokenStoreService } from './token-store.service';

@Injectable()
export class AuthenticationService {

    constructor(
        private http: HttpClient,
        private tokenStore: TokenStoreService
    ) { }

    public isAuthenticated(): boolean {
        return !!this.tokenStore.token;
    }

    public login(username: string, password: string): Observable<boolean> {
        return this.http.post('/api/token', { username, password }, { responseType: 'text' })
            .map(token => {
                this.tokenStore.token = token;
                return !!token;
            });
    }

}
