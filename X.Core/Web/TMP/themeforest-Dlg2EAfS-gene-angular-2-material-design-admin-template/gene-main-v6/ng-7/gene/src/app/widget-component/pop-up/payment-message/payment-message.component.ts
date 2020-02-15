import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'ms-payment-message',
  templateUrl: './payment-message.component.html',
  styleUrls: ['./payment-message.component.scss']
})
export class PaymentMessageComponent implements OnInit {

	paymentMessage : string; 

	constructor( public dialogRef : MatDialogRef<PaymentMessageComponent>,
				 public router : Router) { }

	ngOnInit() {
	}

	//To redirect the Course list page.
	onSubmit(){
		var first = location.pathname.split('/')[1];
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/courses/courses-list']);
		}
		else {
			this.router.navigate(['/courses/courses-list']);
		}
		this.dialogRef.close();
	}
}
