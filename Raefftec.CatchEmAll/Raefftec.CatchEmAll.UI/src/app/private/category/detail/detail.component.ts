import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
    selector: 'app-detail',
    templateUrl: './detail.component.html',
    styleUrls: ['./detail.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DetailComponent implements OnInit {

    public model: FormGroup;

    constructor(
        private http: HttpClient,
        private builder: FormBuilder
    ) { }

    ngOnInit() {
        this.model = this.builder.group({
            id: 0,
            number: null,
            name: ''
        });
    }

    public save(): void {
        this.http.post(`/api/category`, this.model.value)
            .subscribe();
    }

    public reset(): void {
        this.model.reset();
    }

}
