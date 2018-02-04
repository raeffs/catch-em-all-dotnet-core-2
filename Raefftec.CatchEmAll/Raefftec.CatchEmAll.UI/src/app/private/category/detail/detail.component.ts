import { Component, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../../../shared/types/category.class';
import { filter } from 'rxjs/operators/filter';
import { mergeMap } from 'rxjs/operators/mergeMap';
import { map } from 'rxjs/operators/map';
import { Observable } from 'rxjs/Observable';

@Component({
    templateUrl: './detail.component.html',
    styleUrls: ['./detail.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DetailComponent {

    public isProcessing: boolean = true;
    public id: number;
    public model: FormGroup;

    constructor(
        private http: HttpClient,
        private builder: FormBuilder,
        private route: ActivatedRoute,
        private changeDetectorRef: ChangeDetectorRef
    ) {
        this.id = this.route.snapshot.params.id;

        this.model = this.builder.group({
            id: 0,
            number: null,
            name: ['', Validators.required]
        });

        if (!!this.id) {
            this.refresh();
        } else {
            this.isProcessing = false;
        }
    }

    public save(): void {
        this.isProcessing = true;
        let source: Observable<Category>;
        if (!!this.id) {
            source = this.http.put<Category>(`/api/category/${this.id}`, this.model.value);
        } else {
            source = this.http.post<Category>(`/api/category`, this.model.value);
        }

        source.pipe(
            map(x => this.model.reset(x)),
            map(() => this.isProcessing = false),
            map(() => this.changeDetectorRef.markForCheck())
        ).subscribe();
    }

    public reset(): void {
        if (!!this.id) {
            this.refresh();
        } else {
            this.model.reset();
        }
    }

    private refresh(): void {
        this.isProcessing = true;
        this.http.get<Category>(`/api/category/${this.id}`).pipe(
            map(x => this.model.reset(x)),
            map(() => this.isProcessing = false),
            map(() => this.changeDetectorRef.markForCheck())
        ).subscribe();
    }

}
