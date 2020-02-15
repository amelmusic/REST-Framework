import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { Router } from '@angular/router';

@Component({
  selector: 'ms-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class SigninComponent implements OnInit {

	constructor( public translate : TranslateService,
					 private pageTitleService : PageTitleService,
					 public router : Router) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Sign In");
	}

	//continueAsGuest method render to the payment page.
	continueAsGuest(){
		var first = location.pathname.split('/')[1];
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/courses/payment']);
		}else {
			this.router.navigate(['/courses/payment']);
		}
	}

	//signIn method render to the course list page.
	signIn(){
		var first = location.pathname.split('/')[1];
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/courses/courses-list']);
		}else {
			this.router.navigate(['/courses/courses-list']);
		}
	}
}
