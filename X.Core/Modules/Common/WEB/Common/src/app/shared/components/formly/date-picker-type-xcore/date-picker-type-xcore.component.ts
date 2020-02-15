import { Component, OnInit, ViewChild, AfterViewInit, TemplateRef, ChangeDetectionStrategy } from '@angular/core';
import { MatInput, MatDatepickerInput } from '@angular/material';
import { FieldType } from '@ngx-formly/material';
import { defineHiddenProp } from '@ngx-formly/core/lib/utils';

@Component({
  selector: 'ms-date-picker-type-xcore',
  templateUrl: './date-picker-type-xcore.component.html',
  styleUrls: ['./date-picker-type-xcore.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DatePickerTypeXCoreComponent extends FieldType implements AfterViewInit  {

  @ViewChild(MatInput, <any> { static: true }) formFieldControl!: MatInput;
  @ViewChild(MatDatepickerInput, <any> { static: true }) datepickerInput!: MatDatepickerInput<any>;
  @ViewChild('datepickerToggle', <any> { static: true }) datepickerToggle!: TemplateRef<any>;

  defaultOptions = {
    templateOptions: {
      datepickerOptions: {
        startView: 'month',
        datepickerTogglePosition: 'suffix',
      },
    },
  };

  ngAfterViewInit() {
    super.ngAfterViewInit();
    // temporary fix for https://github.com/angular/material2/issues/6728
    (<any> this.datepickerInput)._formField = this.formField;
    console.log('thild', this.formControl);
    (<any> this.datepickerInput).formControl = this.formControl;
    setTimeout(() => {
      // defineHiddenProp(this.field, '_mat' + this.to.datepickerOptions.datepickerTogglePosition, this.datepickerToggle);
      // (<any> this.options)._markForCheck(this.field);
    });
  }
}
