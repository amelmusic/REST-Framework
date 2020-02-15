import { Component, OnInit,ViewEncapsulation }      from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-radio',
   templateUrl:'./radio-material.html',
   styleUrls: ['./radio-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})
export class RadioComponent implements OnInit {

   favoriteSeason : string;

   seasons = [
      'Winter',
      'Spring',
      'Summer',
      'Autumn'
   ];

   constructor( private pageTitleService: PageTitleService, 
                private translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Radio Buttons");
   }

}


