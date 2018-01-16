import { Component, ChangeDetectionStrategy } from '@angular/core';
import { AuthenticationService } from '../../core/services/authentication.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent {

    public username: string = '';

    public password: string = '';

    constructor(
        private authenticationService: AuthenticationService,
        private router: Router
    ) { }

    public login(): void {
        this.authenticationService.login(this.username, this.password)
            .subscribe(success => {
                if (success) {
                    this.router.navigate(['']);
                }
            });
    }


}
