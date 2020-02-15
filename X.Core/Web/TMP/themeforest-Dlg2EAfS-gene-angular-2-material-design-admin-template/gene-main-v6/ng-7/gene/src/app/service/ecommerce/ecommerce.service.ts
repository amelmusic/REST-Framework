import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MatDialog } from '@angular/material';
import { DeleteDialogComponent } from '../../widget-component/pop-up/delete-dialog/delete-dialog.component';
import { AddNewCardComponent } from '../../widget-component/pop-up/add-new-card/add-new-card.component';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

@Injectable({
	providedIn: 'root'
})

export class EcommerceService {

	localStorageProduct : any;
	cartProductLength : number;
	localStorageCard : any;

	/*
	 *  Default Product Cart Data 
	 */
	productCartData:any[]=[
		{   
			productId:1,
			image:"assets/img/product.jpg",
			name:"3DR - Solo Smart Rechargeable Battery - Black",
			quantity:1,
			price:9
		},
		{
			productId:2,
			image:"assets/img/product1.jpg",
			name:"JÄRA Lamp shade, white",
			quantity:1,
			price:15
		},
		{
			productId:3,
			image:"assets/img/product2.JPG",
			name:"LEGITIM-Chopping board, white",
			quantity:1,
			price:23
		},
		{
			productId:4,
			image:"assets/img/product3.jpeg",
			name:"Sour cream and onion potato chips",
			quantity:1,
			price:21
		}
	]

	/*
	 *  Default Number Card Data.
	 */
	numberCardsData:any[]=[
		{
			productId:1,
			number:"5105105105105100",
			fullName:"John Brown"
		},
		{
			productId:2,
			number:"4111111111111111",
			fullName:"Jennifier Meranda"
		},
		{
			productId:3,
			number:"4012888888881881",
			fullName:"Johnson"
      },
      {
			productId:2,
			number:"4111111111111111",
			fullName:"Jennifier Meranda"
		},
		{
			productId:3,
			number:"4012888888881881",
			fullName:"Johnson"
		}
	]
	

	constructor( private toastr : ToastrService,
		private dialog : MatDialog, 
		private http : HttpClient ) {
		this.defaultStorageLocalProduct();
		this.defaultStorageLocalCards();
	}

	//Default Product Cart Data.
	defaultStorageLocalProduct() {
		localStorage.setItem("cartProduct",JSON.stringify(this.productCartData));
		this.getLocalCart();
	}

   //Get Cart Data and Cart length from Local Storage.
	getLocalCart(){
		this.localStorageProduct = JSON.parse(localStorage.getItem("cartProduct"));
		this.cartProductLength = this.localStorageProduct.length;
	}

	
	//Adding new Product to cart in localStorage.
	addToCart(cartProducts){
		let products: any = JSON.parse(localStorage.getItem("cartProduct"));
		let found = products.some(function (products) {
			if(products.name == cartProducts.name){
				return  true;
			}
		});
		if (!found) { products.push(cartProducts); 
			this.toastr.success("Added To Cart");
		}
		localStorage.setItem("cartProduct", JSON.stringify(products)); 
		this.getLocalCart();
	}

	//Set Cart Quantity.
	setCartQuantity(cartProduct){
		let products: any = JSON.parse(localStorage.getItem("cartProduct"));
		for (let i = 0; i < products.length; i++) {
			if (products[i].productId === cartProduct.productId) {
				 products[i].quantity = cartProduct.quantity;
			}
		}
		localStorage.setItem("cartProduct", JSON.stringify(products)); 
	}

	//Delete the product from Local Storage.
	localStorageDelete(cartProduct:any,key:string){
		let products: any = JSON.parse(localStorage.getItem(key));
			for (let i = 0; i < products.length; i++) {
				if (products[i].name === cartProduct.name) {
					products.splice(i, 1);
					this.toastr.success("Item Deleted");
					break;
				}
			}
		localStorage.setItem(key, JSON.stringify(products));
		this.getLocalCart();
	}

	//deleteDiaglog function is used to open the Delete Dialog Component. 
	deleteDialog(data:string){
		let dialogRef : MatDialogRef<DeleteDialogComponent>;
		dialogRef = this.dialog.open(DeleteDialogComponent);
		dialogRef.componentInstance.data = data;
		
		return dialogRef.afterClosed();
	}

	/*
		----------  Number Card Function  ----------
	*/

	//Get NumberCard Items from local storage.
	getLocalCard(){
		this.localStorageCard = JSON.parse(localStorage.getItem("numberCard"));
	}

	//Default Number Card Data.
	defaultStorageLocalCards(){     
		localStorage.setItem("numberCard",JSON.stringify(this.numberCardsData));
		this.getLocalCard();
	}

	//Adding new Number Card in localStorage.
	localStorageAdd(cardAdd){
		let cardNumeber: any = JSON.parse(localStorage.getItem("numberCard"));
		let found = cardNumeber.some(function (cardNumeber) {
			if(cardNumeber.number == cardAdd.number){
				return  true;
			}
		});
		if (!found) { 
			cardNumeber.push(cardAdd);
			this.toastr.success("New Card Added");
		}    
		localStorage.setItem("numberCard", JSON.stringify(cardNumeber));
		this.getLocalCard();
	}  

	//delet the number card in localStorage.
	localStorageDeleteCard(cardNumber){
		let cards: any = JSON.parse(localStorage.getItem("numberCard"));
			for (let i = 0; i < cards.length; i++) {
				if (cards[i].number === cardNumber.number) {
					cards.splice(i, 1);
					this.toastr.success("Number Card Deleted");
					break;
				}
			}
		localStorage.setItem("numberCard", JSON.stringify(cards));
		this.getLocalCard();
	}

	// addNumberCard is used to open the add number card Dialog Component.
	addNumberCard(){
		let dialogRef : MatDialogRef<AddNewCardComponent>;
		dialogRef = this.dialog.open(AddNewCardComponent);

		return dialogRef.afterClosed();
	}

	//getCountries used to get the country data from json file.
	getCountries(){
		return this.http.get('assets/data/countries.json').map(response=>response);
	}

}

