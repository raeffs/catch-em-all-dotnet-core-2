import { Component, ChangeDetectionStrategy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Page, PaginationHelper } from '../../../shared/types/page.class';
import { Category } from '../../../shared/types/category.class';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListComponent {

    public model: BehaviorSubject<PaginationHelper<Category>> = new BehaviorSubject<PaginationHelper<Category>>(new PaginationHelper());

    constructor(
        private http: HttpClient
    ) {
        this.loadPage(1);
    }

    public nextPage(): void {
        this.loadPage(this.model.value.nextPage);
    }

    public previousPage(): void {
        this.loadPage(this.model.value.previousPage);
    }

    public select(item: Category) {
        item.selected = !item.selected;
    }

    private loadPage(page: number): void {
        this.http.get<Page<Category>>('/api/category', { params: { page: page.toString() } })
            .subscribe(model => this.model.next(new PaginationHelper(model)));
    }
}
