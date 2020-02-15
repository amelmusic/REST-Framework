import { Component, ViewChild, OnInit ,ViewEncapsulation} from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";

@Component({
   selector: 'ms-paging-table',
   templateUrl:'./table-paging-component.html',
   styleUrls: ['./table-paging-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class PagingTableComponent implements OnInit {

   rows = [];

   constructor(private pageTitleService: PageTitleService) {
      this.fetch((data) => {
         this.rows = data;
      });
   }

   ngOnInit() {
      this.pageTitleService.setTitle("Paging");
   }

   /**
     * To fetech the data from company.json file.
     */
   fetch(cb) {
      const req = new XMLHttpRequest();
      req.open('GET', `assets/data/company.json`);

      req.onload = () => {
         cb(JSON.parse(req.response));
      };

      req.send();
   }

}



