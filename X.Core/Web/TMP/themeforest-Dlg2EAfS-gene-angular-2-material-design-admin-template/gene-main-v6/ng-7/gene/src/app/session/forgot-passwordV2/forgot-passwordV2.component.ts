import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth-service/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-forgot-password',
   templateUrl:'./forgot-passwordV2-component.html',
   styleUrls: ['./forgot-passwordV2-component.scss'],
   encapsulation: ViewEncapsulation.None,
})
export class ForgotPasswordV2Component {
	
   email  : string;
 
   constructor( public authService: AuthService,
                public router : Router,
                private translate : TranslateService) { }

   /**
     * send method is used to send a reset password link into your email.
     */
   send(value) {
      this.authService.resetPasswordV2(value);
   }   
}



