import { Component, OnInit,ViewEncapsulation} from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";

@Component({
    selector: 'ms-editing-table',
    templateUrl:'./table-editing-component.html',
    styleUrls: ['./table-editing-component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class EditingTableComponent implements OnInit {

   editing = {};
   rows = [];

   constructor(private pageTitleService: PageTitleService) {
      this.fetch((data) => {
         this.rows = data;
     });
   }

   ngOnInit() {
     this.pageTitleService.setTitle("Editing");
   }

   /**
     * To fetch the data from JSON file.
     */
   fetch(cb) {
     const req = new XMLHttpRequest();
     req.open('GET', `assets/data/company.json`);

     req.onload = () => {
       cb(JSON.parse(req.response));
     };

     req.send();
   }

   /**
     * updateValue is used to update the data when edit.
     */
   updateValue(event, cell, rowIndex) {
      this.editing[rowIndex + '-' + cell] = false;
      this.rows[rowIndex][cell] = event.target.value;
      this.rows = [...this.rows];
   }
}



