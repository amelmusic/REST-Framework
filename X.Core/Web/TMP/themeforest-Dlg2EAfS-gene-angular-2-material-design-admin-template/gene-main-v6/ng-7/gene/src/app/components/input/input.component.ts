import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation } from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-input',
   templateUrl:'./input-material.html',
   styleUrls: ['./input-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})
export class InputComponent implements OnInit{

	constructor( private pageTitleService: PageTitleService, 
                public translate : TranslateService) {}

	ngOnInit() {
 	   this.pageTitleService.setTitle("Input");
	}
 	
}


