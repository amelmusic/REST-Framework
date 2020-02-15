import { Component, OnInit ,ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../service/auth-service/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-login-session',
   templateUrl:'./login-component.html',
   styleUrls: ['./login-component.scss'],
   encapsulation: ViewEncapsulation.None,
})
export class LoginComponent {
	
  email    : string ="demo@example.com";
  password : string = "0123456789";

  constructor( public authService: AuthService,
               public translate : TranslateService ) { }

   // when email and password is correct, user logged in.
   login(value) {
      this.authService.loginUser(value);
   }
	
}



