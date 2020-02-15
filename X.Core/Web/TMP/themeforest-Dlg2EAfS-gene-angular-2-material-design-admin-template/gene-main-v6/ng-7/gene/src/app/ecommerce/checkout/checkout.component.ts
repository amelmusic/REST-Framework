import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder} from '@angular/forms';
import { EcommerceService } from '../../service/ecommerce/ecommerce.service';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

	billingForm 	      : FormGroup;
	displayedColumnsData : string [] = ['image','name','quantity','total'];
   countries 				: any; 
   
	constructor(private formBuilder : FormBuilder,
		public ecommerceService : EcommerceService,
		private pageTItleService : PageTitleService,
		private translate : TranslateService) { }

	ngOnInit() {
		this.billingForm = this.formBuilder.group({  
			name  		: [''],
			lastName		: [''],
			email			: [''],
			mobile		: [''],
			address1		: [''],
			address2		: [''],
			country		: [''],
			state			: [''],
			city			: [''],
			validate		: ['']
		});

		this.pageTItleService.setTitle("Checkout");
		this.getCountry();
	}

	/**
	  * calculate method is used to calculate the total price of product item. 
	  */
	calculate(){
		let total:number=0;
		for(let data of this.ecommerceService.localStorageProduct)
			{	
				total+=(data.price * data.quantity);	
			}
		return total;		
	}

	/**
	  * onQuantityClick method is used when quantity of product will change then total price of product is also change. 
	  */
	onQuantityClick(cartProduct){
		let total = null;
		if(cartProduct)
			{
				total = cartProduct.quantity * cartProduct.price;
        	}
      return total;
   }

   /**
	  * getCountry is used to get the Country Data from JSON file.
	  */
	getCountry(){
		this.ecommerceService.getCountries().
			subscribe( res => { this.countries = res['countries']},
						  err => console.log(err),
						  ()  => this.countries
						);
	}

}
