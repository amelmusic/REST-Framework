import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";

@Component({
   selector: 'ms-dashboard',
   templateUrl:'./saas-component.html',
   styleUrls: ['./saas-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class SaasComponent implements OnInit  {

   constructor( private pageTitleService: PageTitleService) {
   }
  
   ngOnInit() {
      this.pageTitleService.setTitle("Home");
   }
}
      
