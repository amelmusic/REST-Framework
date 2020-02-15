import { Component, OnInit, ViewEncapsulation  } from '@angular/core';
import { FormControl, FormGroup, FormBuilder,FormArray, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { CoreService } from '../../service/core/core.service';

@Component({
	selector: 'ms-payment',
	templateUrl: './payment.component.html',
	styleUrls: ['./payment.component.scss'],
	encapsulation: ViewEncapsulation.None
})

export class PaymentComponent implements OnInit{

	paymentForm 	: FormGroup;
	paymentMessage : string = "Thank You, Your payment has been processed successfully.";

	constructor(public formBuilder : FormBuilder,
					private coreService : CoreService,
					private translate : TranslateService,
					private pageTitleService : PageTitleService) { }

	ngOnInit() {
		this.paymentForm = this.formBuilder.group({
			card_number 	  : ['', Validators.required],
			user_card_name   : ['',Validators.required],
			cvv 			     : ['',Validators.required],
			expiry_date 	  : ['',Validators.required]
		});

		this.pageTitleService.setTitle("Payment");
	}
	
	//onSubmit method is used to submit the payment form.
	onSubmit() {
		if(this.paymentForm.valid){
			this.coreService.paymentDialog(this.paymentMessage);
		}
	}

	//onClear method is used to clear the payment form.
	onClear() {
		this.paymentForm.reset();
	}

}
