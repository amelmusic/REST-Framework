import { Component, OnInit } from '@angular/core';
import { FieldType } from '@ngx-formly/material';

@Component({
  selector: 'ms-formly-tinymce',
  templateUrl: './formly-tinymce.component.html',
  styleUrls: ['./formly-tinymce.component.scss']
})
export class FormlyTinymceComponent  extends FieldType implements OnInit {


  ngOnInit() {

    super.ngOnInit();
  }
  
}
