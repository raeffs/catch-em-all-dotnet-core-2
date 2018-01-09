import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationGuard } from './shared/guards/authentication.guard';

const routes: Routes = [
    {
        path: 'public',
        loadChildren: './public/public.module#PublicModule'
    },
    {
        path: 'private',
        loadChildren: './private/private.module#PrivateModule',
        canActivate: [
            AuthenticationGuard
        ]
    },
    {
        path: '**',
        redirectTo: 'private'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
