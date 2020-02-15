import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { EcommerceService } from '../../service/ecommerce/ecommerce.service';
import { PageTitleService }  from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.scss']
})

export class CardComponent implements OnInit {

	popupDeleteRespone : any;
	popupAddNumberCard : any;

	constructor(public ecommerceService : EcommerceService,
		private pageTItleService : PageTitleService,
		private translate : TranslateService) { }

	ngOnInit() {
		this.pageTItleService.setTitle("Card");
	}

	/**
	  * onRemoveCard is used to remove the product Number Card.
	  */
	onRemoveCard(productNumberCard){
		this.ecommerceService.deleteDialog('Are you sure you want to delete this card permanently?').
			subscribe( res=>{ this.popupDeleteRespone = res },
						  err=>console.log(err),
				        ()=>this.getPopupDeleteResponse(this.popupDeleteRespone,productNumberCard)
				      );
	}

	/**
	  * In which when delete response yes then number card delete from local storage.
	  */
	getPopupDeleteResponse(response:any,numberCard){
		if(response == "yes")
		{
			this.ecommerceService.localStorageDeleteCard(numberCard);
			this.ecommerceService.getLocalCard();
		}
	}

	/**
	  * addNewCard is used to add the new number card.
	  */
	addNewCard(){
		this.ecommerceService.addNumberCard().
			subscribe( res => {this.popupAddNumberCard = res},
						  err => console.log(err),
						  ()  => this.getPopupResponse(this.popupAddNumberCard))
   }

	/**
	  * getPopupResponse is used to get the Response. if response will be get then number card add into local storage. 
	  */
	getPopupResponse(response:any){
		if(response)
		{
			this.ecommerceService.localStorageAdd(response);
		}
	}

	/**
	  * replaceCardNumber is used to replace the card number with 'x'.
	  */
	replaceCardNumber(number) {
		let cardNumber = null;
		cardNumber = number.replace(/.(?=.{4})/g, 'x');
		return cardNumber;
	}

}
