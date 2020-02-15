import { Component, OnInit,ViewEncapsulation }      from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-slider',
   templateUrl:'./slider-material.html',
   styleUrls: ['./slider-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class SliderOverviewComponent implements OnInit {

   demo: number;
   val = 50;
   min = 0;
   max = 100;
   
   constructor( private pageTitleService: PageTitleService, 
                private translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Slider");
   }
}


