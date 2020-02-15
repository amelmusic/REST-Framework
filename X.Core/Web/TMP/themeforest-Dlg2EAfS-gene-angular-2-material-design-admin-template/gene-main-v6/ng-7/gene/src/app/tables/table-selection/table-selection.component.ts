import { Component, ViewChild, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";

@Component({
   selector: 'ms-selection-table',
   templateUrl:'./table-selection-component.html',
   styleUrls: ['./table-selection-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class SelectionTableComponent implements OnInit {

   rows: any[] = [];
   selected: any[] = [];
   columns: any[] = [
      { prop: 'name'} , 
      { name: 'Company'}, 
      { name: 'Gender' }
   ];

   constructor(private pageTitleService: PageTitleService) {
      this.fetch((data) => {
         this.rows = data;
      });
   }

   ngOnInit() {
      this.pageTitleService.setTitle("Selection");
   }

   /**
     * To fetch the data from company.json file.
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



