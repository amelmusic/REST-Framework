import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth-service/auth.service';
import { ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-loginV2-session',
   templateUrl:'./loginV2-component.html',
   styleUrls: ['./loginV2-component.scss'],
   encapsulation: ViewEncapsulation.None
})
export class LoginV2Component {

   email    : string = "demo@example.com";
   password : string = "0123456789";

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
                public translate : TranslateService ) { }

   // when email and password is correct, user logged in.
   login(value) {
      this.authService.loginUser(value);
   }
}



