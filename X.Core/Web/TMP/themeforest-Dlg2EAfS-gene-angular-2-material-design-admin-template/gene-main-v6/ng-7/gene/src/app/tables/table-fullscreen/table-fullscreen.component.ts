import { Component, OnInit, ViewEncapsulation  } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";

@Component({
   selector: 'ms-fullscreen-table',
   templateUrl:'./table-fullscreen-component.html',
   styleUrls: ['./table-fullscreen-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class FullscreenTableComponent implements OnInit {

   rows = [];

   constructor(private pageTitleService: PageTitleService) {
      this.fetch((data) => {
         
         this.rows = data;
      });
   }

   ngOnInit() {
      this.pageTitleService.setTitle("Full Screen");
   }

   /**
     * To fetch the data from 100k.json fileT
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



