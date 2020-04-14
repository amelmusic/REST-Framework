import {Component, HostBinding, OnInit} from '@angular/core';
import {FieldArrayType} from '@ngx-formly/core';

@Component({
  selector: 'app-formly-repeat',
  templateUrl: './formly-repeat.component.html',
  styleUrls: ['./formly-repeat.component.scss']
})
export class FormlyRepeatComponent extends FieldArrayType implements OnInit {
    // TODO: add explicit constructor
    constructor() {
      super();
    }
    @HostBinding('class') class = 'col-xs-12';


    ngOnInit() {

    }
}
