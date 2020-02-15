import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'ms-course-detail-billing',
  templateUrl: './course-detail-billing.component.html',
  styleUrls: ['./course-detail-billing.component.scss']
})

export class CourseDetailBillingComponent implements OnInit {

	@Input() billingDetails : any = [];

	constructor(public router : Router) { }

	ngOnInit() {
	}

  //render to the signin page.
	addToCart(){
		var first = location.pathname.split('/')[1];
      if(first == 'horizontal'){
         this.router.navigate(['/horizontal/courses/signin']);
      }else {
         this.router.navigate(['/courses/signin']);
      }
	}
}
