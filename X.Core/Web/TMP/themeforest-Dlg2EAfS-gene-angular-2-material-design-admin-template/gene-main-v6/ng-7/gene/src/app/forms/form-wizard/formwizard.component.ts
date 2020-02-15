import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-formwizard',
   templateUrl:'./formwizard-component.html',
   styleUrls: ['./formwizard-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class FormWizardComponent  implements OnInit{

   selectedIndex: number = 0;

   constructor( private pageTitleService: PageTitleService,
                
                private translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Form Wizard");
   }

   /**
     * nextStep method is used to go to the next step of form.
     */
   nextStep() {
      this.selectedIndex += 1;
   }

   /**
     * previousStep method is used to go to the previous step of form.
     */
   previousStep() {
      this.selectedIndex -= 1;
   }   
}



