import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'ms-add-new-client',
  templateUrl: './add-new-client.component.html',
  styleUrls: ['./add-new-client.component.scss']
})
export class AddNewClientComponent implements OnInit {

	addNewUserForm    : FormGroup;
	emailPattern 		: string = "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$";

	constructor( private formBuilder : FormBuilder,
					 public dialogRef    : MatDialogRef<AddNewClientComponent>) { }

	ngOnInit() {
		this.addNewUserForm = this.formBuilder.group({
			name 	     : ['',[Validators.required]],
			emailAddress : ['',[Validators.required,Validators.pattern(this.emailPattern)]],
			phoneNumber  : ['',[Validators.required]],
			location     : ['',[Validators.required]],
		})
	}

	// onFormSubmit method is submit a add new user form.
	onFormSubmit(){
		this.dialogRef.close(this.addNewUserForm.value);
	}
}