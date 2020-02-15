import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'ms-add-new-user',
  templateUrl: './add-new-user.component.html',
  styleUrls: ['./add-new-user.component.scss']
})
export class AddNewUserComponent implements OnInit {

	addNewUserForm    : FormGroup;
	emailPattern 		: string = "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$";

	constructor( private formBuilder : FormBuilder,
					 public dialogRef    : MatDialogRef<AddNewUserComponent>) { }

	ngOnInit() {
		this.addNewUserForm = this.formBuilder.group({
			firstName	 : ['',[Validators.required]],
			lastName 	 : ['',[Validators.required]],
			emailAddress : ['',[Validators.required,Validators.pattern(this.emailPattern)]],
			accountType  : ['',[Validators.required]]
		})
	}

	// onFormSubmit method is submit a add new user form.
	onFormSubmit(){
		this.dialogRef.close(this.addNewUserForm.value);
	}

}
