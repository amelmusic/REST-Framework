import { Component, ViewChild, OnInit ,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-responsive-table',
   templateUrl:'./table-responsive-component.html',
   styleUrls: ['./table-responsive-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class ResponsiveTableComponent implements OnInit {

   constructor(private pageTitleService: PageTitleService,private translate : TranslateService) {  }

   ngOnInit() {
      this.pageTitleService.setTitle("Responsive");
   }

}



