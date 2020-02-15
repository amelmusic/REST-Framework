import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation } from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-buttons',
   templateUrl:'./buttons-material.html',
   styleUrls: ['./buttons-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})
export class ButtonsComponent implements OnInit{
	
	constructor( private pageTitleService: PageTitleService, 
                private translate : TranslateService) {}

	ngOnInit() {
 	   this.pageTitleService.setTitle("Buttons");
	}
}
