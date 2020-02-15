import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { CoreService } from '../../service/core/core.service';

@Component({
	selector: 'app-products',
	templateUrl: './products.component.html',
	styleUrls: ['./products.component.scss']
})

export class ProductsComponent implements OnInit {
	
	products 			: any;
	productContent	   : any;
	showType	    	   : string = 'grid';

	constructor(public translate : TranslateService,
					private router : Router, 
					private coreService : CoreService) { }

	ngOnInit() {
		this.coreService.getProductContent()
			.subscribe(res => this.getProductResponse(res));
	}
	//getProductResponse method is used to get the respone of all products.
 	public getProductResponse(response) {
      this.products = null;
      let products = ((response.men.concat(response.women)).concat(response.gadgets)).concat(response.accessories);
      this.products = products;
   }

  	/**
	  * productShowType method is used to select the show type of product.
	  */
	productShowType(type) {
		this.showType = type;
		if(type == 'list'){
			document.getElementById('list').classList.add("active");
			document.getElementById('grid').classList.remove("active");
		}
		else{
			document.getElementById('grid').classList.add("active");
			document.getElementById('list').classList.remove("active");
		}
	}
  
}
