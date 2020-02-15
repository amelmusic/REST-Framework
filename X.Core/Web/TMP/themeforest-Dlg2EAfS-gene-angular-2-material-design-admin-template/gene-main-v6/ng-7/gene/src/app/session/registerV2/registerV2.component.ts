import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../service/auth-service/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
	 selector: 'ms-register-session',
	 templateUrl:'./registerV2-component.html',
	 styleUrls: ['./registerV2-component.scss'],
	 encapsulation: ViewEncapsulation.None,
})
export class RegisterV2Component {
	
	name		: string;
	email		: string;
	password	: string;

   slideConfig = {"slidesToShow": 1, "slidesToScroll": 1,"autoplay": true, "autoplaySpeed": 1000,"dots":false,"arrows":false};

   sessionSlider : any [] = [
      {
         image : "assets/img/login-slider1.jpg",
         name  : "Francisco Abbott",
         designation : "CEO-Gene",
         content : "Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy."
      },
      {
         image : "assets/img/login-slider2.jpg",
         name  : "Samona Brown",
         designation : "Designer",
         content : "Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy."
      },
      {
         image : "assets/img/login-slider3.jpg",
         name  : "Anna Smith",
         designation : "Managing Director",
         content : "Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy."
      }
   ]

	constructor( public authService: AuthService,
                public translate : TranslateService) { }

   //register method is used to sign up on the template.
	register(value) {
		this.authService.signupUserProfile(value);
	}
	
}