import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from './services/authentication.service';
import { AuthenticationGuard } from './guards/authentication.guard';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import { TokenStoreService } from './services/token-store.service';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule
    ]
})
export class CoreModule {

    static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
                TokenStoreService,
                AuthenticationService,
                AuthenticationGuard,
                {
                    provide: HTTP_INTERCEPTORS,
                    useClass: AuthenticationInterceptor,
                    multi: true
                }
            ]
        };
    }

}
