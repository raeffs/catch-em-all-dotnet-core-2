import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PrivateComponent } from './private.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { CategoryModule } from './category/category.module';
import { QueryModule } from './query/query.module';

export function loadDashboardModule() {
    return DashboardModule;
}

export function loadCategoryModule() {
    return CategoryModule;
}

export function loadQueryModule() {
    return QueryModule;
}

const routes: Routes = [
    {
        path: '',
        component: PrivateComponent,
        children: [
            { path: 'dashboard', loadChildren: loadDashboardModule },
            { path: 'category', loadChildren: loadCategoryModule },
            { path: 'query', loadChildren: loadQueryModule },
            { path: '**', redirectTo: 'dashboard' }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PrivateRoutingModule { }
