import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Page } from '../../../shared/types/page.class';
import { Category } from '../../../shared/types/category.class';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css'],
    //changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListComponent implements OnInit {

    public model: Observable<Page<Category>>;

    constructor(
        private http: HttpClient
    ) { }

    ngOnInit() {
        this.model = this.http.get<Page<Category>>('/api/category');
    }
}
