import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Page } from '../../../shared/types/page.class';
import { Category } from '../../../shared/types/category.class';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListComponent implements OnInit {

    constructor(
        private http: HttpClient
    ) { }

    ngOnInit() {
        this.http.get<Page<Category>>('/api/category')
            .subscribe(page => {
                console.log(page);
            })
    }
}
