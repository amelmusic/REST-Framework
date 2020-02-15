import { Component, OnInit,ViewEncapsulation}      from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-tooltip',
   templateUrl:'./tooltip-material.html',
   styleUrls: ['./tooltip-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class TooltipOverviewComponent implements OnInit {

   constructor( private pageTitleService: PageTitleService, 
                private translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Tooltip");
   }

}


