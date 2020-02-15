import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
	selector: 'app-add-product',
	templateUrl: './add-product.component.html',
	styleUrls: ['./add-product.component.scss']
})

export class AddProductComponent implements OnInit {
	
   form			  : FormGroup;
   mainImgPath   : string;
	colorsArray   : string[] = ["Red", "Blue", "Yellow", "Green"];
   sizeArray     : number[] = [36,38,40,42,44,46,48];
   quantityArray : number[] = [1,2,3,4,5,6,7,8,9,10];
   public imagePath;

   "data" : any = [
      {  
         "image": "https://via.placeholder.com/625x800",
         "image_gallery": [
            "https://via.placeholder.com/625x800",
            "https://via.placeholder.com/625x800",
            "https://via.placeholder.com/625x800",
            "https://via.placeholder.com/625x800",
            "https://via.placeholder.com/625x800"
         ]
      }
   ]

	constructor(public formBuilder : FormBuilder,
               public router : Router) { }

	ngOnInit() {

      this.mainImgPath = this.data[0].image;
      this.form = this.formBuilder.group({
			name					: [],
			price 				: [],
			availablity   		: [],
			product_code 		: [],
			description 		: [],
			tags					: [],
			features				: []
      });
   }

   /**
    * getImagePath is used to change the image path on click event. 
    */
   public getImagePath(imgPath: string, index:number) {
      document.querySelector('.border-active').classList.remove('border-active');
      this.mainImgPath = imgPath;
      document.getElementById(index+'_img').className += " border-active";
   }


   // backToProduct method redirect to the Edit products page.
   backToProduct(){
      var first = location.pathname.split('/')[1];
      if(first == 'horizontal'){
         this.router.navigate(['/horizontal/ecommerce/edit-products']);
      }else {
         this.router.navigate(['/ecommerce/edit-products']);
      }
   }
}
