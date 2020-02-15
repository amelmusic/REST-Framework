import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef,MatDialog} from '@angular/material';

@Component({
  selector: 'ms-edit-new-user',
  templateUrl: './edit-new-user.component.html',
  styleUrls: ['./edit-new-user.component.scss']
})
export class EditNewUserComponent implements OnInit {

	form : FormGroup
	data : any;

	constructor( public formBuilder : FormBuilder,
					 public dialogRef : MatDialogRef<EditNewUserComponent>) { }

	ngOnInit() 
		{
		this.form = this.formBuilder.group({
			firstName		: [],
			lastName 		: [],
			emailAddress   : [],
			accountType 	: []
		});

		if(this.data){
			this.form.patchValue({
				firstName    : this.data.firstName,
				lastName		 : this.data.lastName,
				emailAddress : this.data.emailAddress,
				accountType  : this.data.accountType
			});
		}
	}

	/**
	  *onFormSubmit method is used to submit the edit new user dialaog form and close the dialog.
	  */
	onFormSubmit(){
		this.dialogRef.close(this.form.value);
	}
}