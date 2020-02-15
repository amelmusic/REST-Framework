import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-google-map',
   templateUrl:'./googlemap-component.html',
   styleUrls: ['./googlemap-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class GoogleMapComponent implements OnInit {
	
   lat: number = 40.730610;
  	lng: number = -73.935242;

	constructor(private pageTitleService: PageTitleService, private translate : TranslateService) {}

	ngOnInit() {
 	   this.pageTitleService.setTitle("Google Map");
	}

}
