import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-pricing',
   templateUrl: './pricing.component.html',
   styleUrls: ['./pricing.component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]

})

export class PricingComponent implements OnInit {

   pricingContent : any = [
      {
         title : "Standard",
         sub_title : "For most of the users",
         price : 40,
         content : [
            "10GB of Bandwidth",
            "200MB Max File Size",
            "2GHZ CPU",
            "256MB Memory",
            "1GB Storage"
         ]
      },
      {
         title : "Advanced",
         sub_title : "For web expericence users",
         price : 90,
         content : [
            "25GB of Bandwidth",
            "750MB Max File Size",
            "2GHZ CPU",
            "256MB Memory",
            "250GB Storage"
         ]
      },
      {
         title : "Mega",
         sub_title : "For developer",
         price : 170,
         content : [
            "50GB of Bandwidth",
            "200MB Max File Size",
            "756MB CPU",
            "256MB Memory",
            "500GB Storage"
         ]
      },
      {
         title : "Master",
         sub_title : "For large enterprises",
         price : 410,
         content : [
            "60GB of Bandwidth",
            "1BG Max File Size",
            "2GHZ CPU",
            "256MB Memory",
            "1TB Storage"
         ]
      }
   ]

   constructor(private pageTitleService: PageTitleService, private translate : TranslateService) { }

   ngOnInit() {
      this.pageTitleService.setTitle("Pricing");
   }

}
