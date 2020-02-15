import { Component, OnInit,ViewEncapsulation }      from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-select',
   templateUrl:'./select-material.html',
   styleUrls: ['./select-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class SelectComponent implements OnInit{

   selectedValue: string;

   foods = [
      {value: 'steak-0', viewValue: 'Steak'},
      {value: 'pizza-1', viewValue: 'Pizza'},
      {value: 'tacos-2', viewValue: 'Tacos'}
   ];

	constructor(private pageTitleService: PageTitleService, private translate : TranslateService) { }

  	ngOnInit() {
      this.pageTitleService.setTitle("Select");
   }
}


