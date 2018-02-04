import { Component, ChangeDetectionStrategy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Page, PaginationHelper } from '../../../shared/types/page.class';
import { Category } from '../../../shared/types/category.class';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { mergeMap } from 'rxjs/operators/mergeMap';
import { finalize } from 'rxjs/operators/finalize';

@Component({
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListComponent {

    public isProcessing: boolean = true;
    public model: BehaviorSubject<PaginationHelper<Category>> = new BehaviorSubject<PaginationHelper<Category>>(new PaginationHelper());
    public selectionCount: number = 0;
    public allSelected: boolean = false;

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

    public select(item: Category, event: Event) {
        event.preventDefault();
        event.stopPropagation();

        item.selected = !item.selected;
        this.selectionCount = this.model.value.items.filter(x => x.selected).length;
        this.allSelected = this.selectionCount !== 0 && this.selectionCount === this.model.value.items.length;
    }

    public toggleAll(event: Event) {
        event.preventDefault();
        event.stopPropagation();

        this.model.value.items.forEach(x => x.selected = !this.allSelected);
        this.allSelected = !this.allSelected;
        this.selectionCount = this.allSelected ? 1 : 0;
    }

    public deleteSelected(): void {
        this.isProcessing = true;
        const ids = this.model.value.items.filter(x => x.selected).map(x => x.id);
        Observable.of(...ids).pipe(
            mergeMap(x => this.http.delete(`/api/category/${x}`)),
            finalize(() => this.loadPage(this.model.value.currentPage))
        ).subscribe();
    }

    private loadPage(page: number): void {
        this.isProcessing = true;
        this.http.get<Page<Category>>('/api/category', { params: { page: page.toString() } })
            .subscribe(model => {
                this.model.next(new PaginationHelper(model))
                this.selectionCount = 0;
                this.allSelected = false;
                this.isProcessing = false;
            });
    }
}
