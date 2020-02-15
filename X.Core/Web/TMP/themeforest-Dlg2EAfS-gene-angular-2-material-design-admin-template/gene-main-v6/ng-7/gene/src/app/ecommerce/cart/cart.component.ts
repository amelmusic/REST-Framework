import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { EcommerceService } from '../../service/ecommerce/ecommerce.service';
import { Router} from '@angular/router';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})

export class CartComponent implements OnInit {

	displayedColumnsData : string [] = ['image','name','quantity','price','total','icon'];
	popupDeleteResponse  : any;

	constructor(public ecommerceService : EcommerceService,
		public router : Router,
		private pageTitleService : PageTitleService,
		private translate : TranslateService) { }

	ngOnInit() {	
		this.pageTitleService.setTitle("Cart")
	}

	/**
	  * onQuantityClick method is used to set cart quantity and calculate the total.
	  */
	onQuantityClick(cartProduct){
		let total = null;
		if(cartProduct){
			total = cartProduct.quantity * cartProduct.price;
		}
		this.ecommerceService.setCartQuantity(cartProduct);
		return total;
	}


    /**
     * calculate is used to calculate the price of all product into cart. 
     */
	calculate(){
		let total : number = 0;
		for(let data of this.ecommerceService.localStorageProduct)
		{	
			total+=(data.price * data.quantity);	
		}
		return total;		
	}


   /**
     * removeCartProduct is used to remove the product from cart.
     */    
	removeCartProduct(cartProduct){
      this.ecommerceService.deleteDialog('Are you sure you want to delete this product permanently?')
         .subscribe( res => { this.popupDeleteResponse = res },
            			err=>console.log(err),
           				()=>this.getPopupDeleteResponse(this.popupDeleteResponse,cartProduct)
         );
   }

	/**
	  * getPopupDeleteResponse is used to delete the product from cart if response yes.
	  */
	getPopupDeleteResponse(response:any,cartProduct){
		if(response == "yes")
		{ 
			this.ecommerceService.localStorageDelete(cartProduct,"cartProduct");
		}
	}

	/**
	  * checkout mathod is used to open the checkout page. 
	  */
	checkout(){
		var first = location.pathname.split('/')[1];
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/ecommerce/checkout']);
		}else {
			this.router.navigate(['/ecommerce/checkout']);
		}
	}

}
