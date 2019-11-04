import { Component, OnInit } from '@angular/core';
import {FieldType} from '@ngx-formly/core';

@Component({
  selector: 'app-formly-button',
  templateUrl: './formly-button.component.html',
  styleUrls: ['./formly-button.component.scss']
})
export class FormlyButtonComponent  extends FieldType {

    onClick($event) {
        if (this.to.onClick) {
            this.to.onClick($event);
        }
    }

}
