import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PrivateComponent } from './private.component';
import { DashboardModule } from './dashboard/dashboard.module';

export function loadDashboardModule() {
    return DashboardModule;
}

const routes: Routes = [
    {
        path: '',
        component: PrivateComponent,
        children: [
            { path: 'dashboard', loadChildren: loadDashboardModule },
            { path: '**', redirectTo: 'dashboard' }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PrivateRoutingModule { }
