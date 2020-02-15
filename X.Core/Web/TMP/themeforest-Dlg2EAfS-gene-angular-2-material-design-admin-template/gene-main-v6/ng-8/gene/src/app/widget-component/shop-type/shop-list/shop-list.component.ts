import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { EcommerceService } from '../../../service/ecommerce/ecommerce.service';

@Component({
  selector: 'ms-shop-list',
  templateUrl: './shop-list.component.html',
  styleUrls: ['./shop-list.component.scss']
})
export class ShopListComponent implements OnInit {
	
	cartData					 : any[]; 
	@Input() shopGridData : any;
   @Output() addToCart	 : EventEmitter<any> = new EventEmitter<any>();

	constructor( public router : Router,
					 private ecommerceService : EcommerceService) { }

	ngOnInit() {
	}

	/**
	  * addTOCart function is used to add the items into cart
	  */
	addTOCart(shop){
		this.addToCart.emit(shop);
	}

	/**
	  * ifItemExistInCart function is used to check if item is exist into cart or not
	  */
	ifItemExistInCart(shop){
		let exits= false;
		this.cartData = this.ecommerceService.localStorageProduct;
			for(let i = 0; i <this.cartData.length; i++){
				if (this.cartData[i].name === shop.name){
					exits = true;
				}
				else{
					exits =  false;
				}
			}
		return exits;
	}
		
	/**
	  * viewCart is used to view the cart
	  */
	viewCart() {
		var first = location.pathname.split('/')[1];
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/ecommerce/cart']);
		}else {
			this.router.navigate(['/ecommerce/cart']);
		}
	}

}
