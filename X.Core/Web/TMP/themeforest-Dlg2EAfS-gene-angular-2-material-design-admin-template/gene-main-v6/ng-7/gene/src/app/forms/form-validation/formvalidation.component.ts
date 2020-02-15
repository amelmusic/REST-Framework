import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

const password        = new FormControl('', Validators.required);
const confirmPassword = new FormControl('', CustomValidators.equalTo(password));

@Component({
    selector: 'ms-formvalidation',
	  templateUrl:'./formvalidation-component.html',
	  styleUrls: ['./formvalidation-component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})

export class FormValidationComponent implements OnInit {

	public form: FormGroup;
  	
    constructor(private fb: FormBuilder, private pageTitleService: PageTitleService,private translate : TranslateService) {}
  	
    ngOnInit() {
      this.pageTitleService.setTitle("Form Validation");

    	this.form = this.fb.group({
      	fname: [null, Validators.compose([Validators.required, Validators.minLength(5), Validators.maxLength(10)])],
      	email: [null, Validators.compose([Validators.required, CustomValidators.email])],
      	range: [null, Validators.compose([Validators.required, CustomValidators.range([5, 9])])],
      	url: [null, Validators.compose([Validators.required, CustomValidators.url])],
      	date: [null, Validators.compose([Validators.required, CustomValidators.date])],
      	creditCard: [null, Validators.compose([Validators.required, CustomValidators.creditCard])],
        phone: [null, Validators.compose([Validators.required])],
      	gender: ['male', Validators.required],
      	password: password,
      	confirmPassword: confirmPassword
    	});
  	}  
}



