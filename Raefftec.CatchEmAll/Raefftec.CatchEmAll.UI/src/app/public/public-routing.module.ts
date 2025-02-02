import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PublicComponent } from './public.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
    {
        path: '',
        component: PublicComponent,
        children: [
            { path: 'login', component: LoginComponent },
            { path: '**', redirectTo: 'login' }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
