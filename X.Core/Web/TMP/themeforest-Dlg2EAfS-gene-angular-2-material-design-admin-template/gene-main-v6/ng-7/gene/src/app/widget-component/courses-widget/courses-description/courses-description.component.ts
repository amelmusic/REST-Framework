import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'ms-courses-description',
  templateUrl: './courses-description.component.html',
  styleUrls: ['./courses-description.component.scss']
})

export class CoursesDescriptionComponent implements OnInit {
	
	@Input() course;
	
	constructor(private router : Router) { }

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
