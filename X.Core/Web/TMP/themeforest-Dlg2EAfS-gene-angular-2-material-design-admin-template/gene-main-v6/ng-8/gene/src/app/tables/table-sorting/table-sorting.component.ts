import { Component, ViewChild, OnInit ,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";

@Component({
   selector: 'ms-sorting-table',
   templateUrl:'./table-sorting-component.html',
   styleUrls: ['./table-sorting-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class SortingTableComponent implements OnInit {

   rows = [];

   columns = [
      { name: 'Company' },
      { name: 'Name' },
      { name: 'Gender' }
   ];

   constructor(private pageTitleService: PageTitleService) {
      this.fetch((data) => {
         this.rows = data;
      });
   }

   ngOnInit() {
      this.pageTitleService.setTitle("Sorting");
   }

   /**
     * to fetch the data from company.json file.
     */
   fetch(cb) {
      const req = new XMLHttpRequest();
      req.open('GET', `assets/data/company.json`);

      req.onload = () => {
         const data = JSON.parse(req.response);
         cb(data);
      };

      req.send();
   }
}



