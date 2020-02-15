import { Component, OnInit } from '@angular/core';
import { CoreService } from '../../service/core/core.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute, Params }   from '@angular/router';

@Component({
	selector: 'app-edit-product',
	templateUrl: './edit-product.component.html',
	styleUrls: ['./edit-product.component.scss']
})

export class EditProductComponent implements OnInit {

	editProductDetail    : any;
	mainImgPath   			: string;
	productId     			: any;
	productType	 			: any;
	showStatus    			: boolean;
	form			  			: FormGroup;
	colorsArray   			: string[] = ["Red", "Blue", "Yellow", "Green"];
   sizeArray    			: number[] = [36,38,40,42,44,46,48];
   quantityArray 			: number[] = [1,2,3,4,5,6,7,8,9,10];
	
	constructor(private coreService : CoreService,
					public formBuilder : FormBuilder,
					private route: ActivatedRoute,
               private router: Router) { }

	ngOnInit() {
		if(this.coreService.editProductData) {
			this.editProductDetail = this.coreService.editProductData;
			setTimeout(()=>{
				this.mainImgPath = this.editProductDetail.image;
			},0)
		}

		this.route.params.subscribe(res => {
			this.productId = res.id;
         this.productType = res.type;
			this.getEditProductDetail();
      })

   	this.form = this.formBuilder.group({
			name					: [],
			price 				: [],
			availablity   		: [],
			product_code 		: [],
			description 		: [],
			tags					: [],
			features				: []
		});
   	this.getProductData();
	}

	/**
    * getImagePath is used to change the image path on click event. 
    */
   public getImagePath(imgPath: string, index:number) {
      document.querySelector('.border-active').classList.remove('border-active');
      this.mainImgPath = imgPath;
      document.getElementById(index+'_img').className += " border-active";
   }

   //getEditProductDetail method is used to get the edit product.
	public getEditProductDetail() {
      this.productId = (this.productId) ? this.productId : 1;
      this.productId = (this.productId) ? this.productId : 'Business';
      this.coreService.getProductContent().
      	subscribe(res => {this.getProductEditResponse(res)});
   }

   //getProductEditResponse method is used to according to the response edit poroduct data show.
   public getProductEditResponse(response) {
		let products = ((response.men.concat(response.women)).concat(response.gadgets)).concat(response.accessories);
      for(let data of products)
      {	
         if(data.id == this.productId && data.type == this.productType) {
            this.editProductDetail = data;
				this.mainImgPath = this.editProductDetail.image;
				this.getProductData();
            break;
         }
      }
	}

	//getProductData method is used to get the product data.
	getProductData(){
		if(this.editProductDetail){
			this.form.patchValue({
				name   		 : this.editProductDetail.name,
				price 		 : this.editProductDetail.price,
				availablity  : this.editProductDetail.availablity,
				product_code : this.editProductDetail.product_code,
				description  : this.editProductDetail.description,
				tags			 : this.editProductDetail.tags,
				features 	 : this.editProductDetail.features
			});
		}
	}

	/// backToProduct method redirect to the Edit products page.
   backToProduct(){
      var first = location.pathname.split('/')[1];
      if(first == 'horizontal'){
         this.router.navigate(['/horizontal/ecommerce/edit-products']);
      }else {
         this.router.navigate(['/ecommerce/edit-products']);
      }
   }
}
