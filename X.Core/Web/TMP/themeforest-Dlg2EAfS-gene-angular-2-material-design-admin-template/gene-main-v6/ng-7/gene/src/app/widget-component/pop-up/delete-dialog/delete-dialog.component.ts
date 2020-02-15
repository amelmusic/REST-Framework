import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

@Component({
  selector: 'ms-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.scss']
})
export class DeleteDialogComponent implements OnInit {

	data : string;

	constructor(public dialogRef : MatDialogRef<DeleteDialogComponent>){
	} 

	ngOnInit() {
	}

	// yes method is used to close the delete dialog and send the response "yes".
	yes(){
		this.dialogRef.close("yes");
	}
}
