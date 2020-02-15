import { Component, OnInit,ViewEncapsulation }      from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'ms-checkbox',
  	templateUrl:'./checkbox-material.html',
  	styleUrls: ['./checkbox-material.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class CheckboxComponent implements OnInit {
   
   checked       = false;
   indeterminate = false;
   disabled      = false;
   align         = 'start';
   labelPosition = 'after';

   constructor(private pageTitleService: PageTitleService, private translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Checkbox");
   }
}


