import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-lockscreen',
   templateUrl:'./lockscreenV2-component.html',
   styleUrls: ['./lockscreenV2-component.scss'],
   encapsulation: ViewEncapsulation.None,
})

export class LockScreenV2Component {
	
  username : string;
  password : string;

  constructor( private router: Router,
               public translate : TranslateService) { }

  // when submit button click, router navigates to a home page.
  onSubmit() {
    this.router.navigate ( ['/'] );
  }
	
}



