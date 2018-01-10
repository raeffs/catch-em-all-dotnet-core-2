import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { TokenStoreService } from '../services/token-store.service';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

    constructor(
        private tokenStore: TokenStoreService
    ) { }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!!this.tokenStore.token) {
            const header = `Bearer ${this.tokenStore.token}`;
            request = request.clone({ setHeaders: { Authorization: header } });
        }

        return next.handle(request);
    }
}