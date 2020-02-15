import { Component, ViewChild, OnInit ,ViewEncapsulation} from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";

@Component({
    selector: 'ms-pinning-table',
    templateUrl:'./table-pinning-component.html',
    styleUrls: ['./table-pinning-component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class PinningTableComponent implements OnInit{

   rows = [];

   constructor(private pageTitleService: PageTitleService) {
      this.fetch((data) => {
         this.rows = data;
      });
   }

   ngOnInit() {
      this.pageTitleService.setTitle("Pinning");
   }

   /**
     * to fetch the data from 100k.json file.
     */
   fetch(cb) {
      const req = new XMLHttpRequest();
      req.open('GET', `assets/data/100k.json`);

      req.onload = () => {
         cb(JSON.parse(req.response));
      };

      req.send();
   }
}



