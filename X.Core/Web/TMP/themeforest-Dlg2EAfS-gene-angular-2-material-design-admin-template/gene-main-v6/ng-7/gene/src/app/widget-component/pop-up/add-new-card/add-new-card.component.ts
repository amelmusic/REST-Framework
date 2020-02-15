import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-add-new-card',
  templateUrl: './add-new-card.component.html',
  styleUrls: ['./add-new-card.component.scss']
})
export class AddNewCardComponent implements OnInit {

	months					 : any	= [1,2,3,4,5,6,7,8,9,10,11,12];
	expiryYear			  	 : any	= [2018,2019,2020,2021,2022,2023,2024,2025];
	productNumberCardForm    : FormGroup;
	
	constructor(private formBuilder : FormBuilder,
					public dialogRef:MatDialogRef<AddNewCardComponent>,
					private translate : TranslateService) { }

	public myModel = '';
  	public mask = [/\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/]
	
	ngOnInit() {
		this.productNumberCardForm = this.formBuilder.group({
			number		: ['',[Validators.required]],
			fullName 	: ['',[Validators.required]],
			expiryMonth : ['',[Validators.required]],
			expiryYear  : ['',[Validators.required]],
			cvv			: ['',[Validators.required,Validators.maxLength(3),Validators.minLength(3)]]
		})
	}

	//onFormSubmit method is submit a product number card form.
	onFormSubmit(){
		this.dialogRef.close(this.productNumberCardForm.value);
	}
}
