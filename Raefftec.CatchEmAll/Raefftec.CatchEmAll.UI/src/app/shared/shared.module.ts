import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValidationErrorPipe } from './pipes/validation-error.pipe';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [ValidationErrorPipe],
    exports: [ValidationErrorPipe]
})
export class SharedModule { }
