import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../service/auth-service/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
	 selector: 'ms-register-session',
	 templateUrl:'./register-component.html',
	 styleUrls: ['./register-component.scss'],
	 encapsulation: ViewEncapsulation.None,
})

export class RegisterComponent {
	
	name			    : string;
	email			    : string;
	password		    : string;
	passwordConfirm : string;

	constructor( public authService: AuthService,
				    public translate : TranslateService) { }

	//register method is used to sign up on the template.
	register(value) {
		this.authService.signupUserProfile(value);
	}
	
}



