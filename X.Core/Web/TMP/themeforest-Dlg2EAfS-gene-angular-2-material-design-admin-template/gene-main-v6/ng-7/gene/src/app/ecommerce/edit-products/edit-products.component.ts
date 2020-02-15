import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { CoreService } from '../../service/core/core.service';

@Component({
	selector: 'ms-edit-products',
	templateUrl: './edit-products.component.html',
	styleUrls: ['./edit-products.component.scss']
})
export class EditProductsComponent implements OnInit {

	productsList 		      : any;
	productsGrid 			   : any;
	popUpDeleteUserResponse : any;
	productContent				: any;
	showType	    				: string = 'grid';
	displayedProductColumns : string [] = ['id', 'image','name','brand','category', 'product_code', 'discount_price', 'price','action' ];
	@ViewChild(MatPaginator) paginator : MatPaginator;
	@ViewChild(MatSort) sort           : MatSort;

	constructor(public translate : TranslateService,
					private router : Router, 
					private coreService : CoreService) { }

	ngOnInit() {
		this.coreService.getProductContent()
			.subscribe(res => this.getProductResponse(res));
	}
	//getProductResponse method is used to get the respone of all products.
 	public getProductResponse(response) {
      this.productsGrid = null;
      let products = ((response.men.concat(response.women)).concat(response.gadgets)).concat(response.accessories);
      this.productsGrid = products;
   }

  	/**
	  * productShowType method is used to select the show type of product.
	  */
	productShowType(type) {
		this.showType = type;
		if(type == 'list'){
			document.getElementById('list').classList.add("active");
			document.getElementById('grid').classList.remove('active');
			this.productsList = new MatTableDataSource(this.productsGrid);
			setTimeout(()=>{
				this.productsList.paginator = this.paginator;
				this.productsList.sort = this.sort;
			},0)
			
		}
		else{
			document.getElementById('grid').classList.add("active");
			document.getElementById('list').classList.remove('active');
		}
	}

	/**
	  * onEditProduct method is used to open the edit page and edit the product.
	  */
	onEditProduct(data){
		var first = location.pathname.split('/')[1];
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/ecommerce/edit-product',data.type,data.id]);
			this.coreService.editProductData = data;
		}
		else{
			this.router.navigate(['/ecommerce/edit-product',data.type,data.id]);
			this.coreService.editProductData = data;
		} 
		
	}

	/* 
     *deleteProduct method is used to open a delete dialog.
     */
   deleteProduct(i){
      this.coreService.deleteDialog("Are you sure you want to delete this product permanently?").
         subscribe( res => {this.popUpDeleteUserResponse = res},
                    err => console.log(err),
                    ()  => this.getDeleteResponse(this.popUpDeleteUserResponse,i))
   }

   /**
     * getDeleteResponse method is used to delete a product from the product list.
     */
   getDeleteResponse(response : string,i){
      if(response == "yes"){
      	if(this.showType == 'grid') {
         	this.productsGrid.splice(i,1);
      	}else if(this.showType == 'list'){
				this.productsList.data.splice(i,1);
				this.productsList = new MatTableDataSource(this.productsList.data);
      		this.productsList.paginator = this.paginator;
      	}
      }
   }

   //routing navigate to the add product page.
   addProduct(){
   	var first = location.pathname.split('/')[1];
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/ecommerce/product-add']);
		}else {
			this.router.navigate(['/ecommerce/product-add']);
		}
   }
  
}