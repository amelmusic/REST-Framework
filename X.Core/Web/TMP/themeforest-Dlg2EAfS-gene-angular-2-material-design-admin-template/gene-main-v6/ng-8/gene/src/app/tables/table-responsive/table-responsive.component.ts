import { Component, ViewChild, OnInit ,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';
import { CoreService } from '../../service/core/core.service';

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

   responsiveTable : any;

   constructor(private pageTitleService: PageTitleService,
               private translate : TranslateService,
               public service : CoreService) {  }

   ngOnInit() {
      
      this.pageTitleService.setTitle("Responsive");

      this.service.getResponsiveTableContent().
         subscribe( res => { this.responsiveTable = res },
                    err => console.log(err),
                    ()  => this.responsiveTable  )
   }

}



