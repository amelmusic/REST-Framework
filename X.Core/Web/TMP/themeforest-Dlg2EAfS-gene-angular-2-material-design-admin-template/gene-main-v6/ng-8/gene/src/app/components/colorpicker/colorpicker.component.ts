import {Component, OnInit,ViewEncapsulation} from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-color-picker',
   templateUrl:'./colorpicker-material.html',
   styleUrls: ['./colorpicker-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class ColorpickerComponent implements OnInit{

   public color  : string = "#127bdc";
   public color2 : string = "#e53935";
 
   constructor(private pageTitleService: PageTitleService, private translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Color Picker");
   }

}


