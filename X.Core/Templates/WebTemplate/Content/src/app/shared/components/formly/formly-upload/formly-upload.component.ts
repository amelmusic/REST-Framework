import { Component, OnInit } from '@angular/core';
import {FieldType} from '@ngx-formly/material';

@Component({
  selector: 'ms-formly-upload',
  templateUrl: './formly-upload.component.html',
  styleUrls: ['./formly-upload.component.scss']
})
export class FormlyUploadComponent extends FieldType implements OnInit {

  constructor() {
    super();
  }

  ngOnInit() {
  }

}
