import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef,MatDialog} from '@angular/material';

@Component({
  selector: 'ms-edit-new-client',
  templateUrl: './edit-new-client.component.html',
  styleUrls: ['./edit-new-client.component.scss']
})
export class EditNewClientComponent implements OnInit {

 
	form : FormGroup
	data : any;

	constructor( public formBuilder : FormBuilder,
					 public dialogRef : MatDialogRef<EditNewClientComponent>) { }

	ngOnInit() 
		{
		this.form = this.formBuilder.group({
			name				: [],
			country			: [],
			e_mail   		: [],
			phone_number 	: []
		});

		if(this.data){
			this.form.patchValue({
				name    : this.data.name,
				e_mail : this.data.e_mail,
				phone_number  : this.data.phone_number,
				country : this.data.country
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