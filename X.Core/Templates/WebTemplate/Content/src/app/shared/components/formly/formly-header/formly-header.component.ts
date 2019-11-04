import { Component, OnInit } from '@angular/core';
import {FieldType} from '@ngx-formly/core';

@Component({
  selector: 'app-formly-header',
  templateUrl: './formly-header.component.html',
  styleUrls: ['./formly-header.component.scss']
})
export class FormlyHeaderComponent extends FieldType implements OnInit {

  constructor() {
      super();
  }

  ngOnInit() {
  }

}
