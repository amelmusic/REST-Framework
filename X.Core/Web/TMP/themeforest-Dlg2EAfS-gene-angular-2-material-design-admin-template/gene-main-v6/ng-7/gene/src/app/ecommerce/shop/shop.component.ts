import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { EcommerceService } from '../../service/ecommerce/ecommerce.service';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

	shopShowType : string = "list";

	constructor(private ecommerceService : EcommerceService,
		private pageTitleService : PageTitleService,
		private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Shop");
	}

	/**
	  * transformHits return the product rating star.
	  */
	public transformHits(hits) {
		hits.forEach(hit => {
			hit.stars = [];
			for (let i = 1; i <= 5; i) {
			   hit.stars.push(i <= hit.rating);
			   i += 1;
			 }
		});
		return hits;
	}

	/**
	  * addToCart is used to add the product into cart.
	  */
	addToCart(event){
		event.quantity = 1;
		this.ecommerceService.addToCart(event);
	}

	/**
	  * itemsShowType method is used to select the show type of items in the shop.
	  */
	itemsShowType(type) {
		this.shopShowType = type;
	}
}
