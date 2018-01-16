import { Pipe, PipeTransform } from '@angular/core';
import { FormControl } from '@angular/forms';

@Pipe({
    name: 'validationError'
})
export class ValidationErrorPipe implements PipeTransform {

    private static readonly errorMessages: { [key: string]: string } = {
        'required': 'This field is required.'
    };

    public transform(value: FormControl, args?: any): any {
        if (value.valid) {
            return '';
        }

        var errors = Object.keys(value.errors);
        var message = ValidationErrorPipe.errorMessages[errors[0]];

        if (message) {
            return message;
        }

        return errors[0];
    }

}
