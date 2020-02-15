import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation } from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-progress',
   templateUrl:'./progress-material.html',
   styleUrls: ['./progress-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class ProgressComponent implements OnInit{

   value             = 40;
   determinateValue  = 30;
   bufferValue       = 30;
   bufferBufferValue = 40;

   constructor( private pageTitleService: PageTitleService,
                public translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Progress");
   }

   /**
     * step method is used to check the step of progress circle.
     */
   step(val: number) {
      this.value += val;
   }

   /**
     * stepDeterminateVal method is used to check the step of Determinate progress-bar.
     */
   stepDeterminateVal(val: number) {
      this.determinateValue += val;
   }

   /**
     * stepBufferVal method is used to check the step of Buffer Progress.
     */
   stepBufferVal(val: number) {
      this.bufferValue += val;
   }

   /**
     * stepBufferBufferVal method is used to check the step of Buffer.
     */
   stepBufferBufferVal(val: number) {
      this.bufferBufferValue += val;
   }

}


