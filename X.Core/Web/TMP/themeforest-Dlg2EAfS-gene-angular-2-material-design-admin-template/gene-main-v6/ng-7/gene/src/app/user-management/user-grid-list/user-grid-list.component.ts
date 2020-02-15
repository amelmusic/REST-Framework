import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';

@Component({
  selector: 'ms-user-grid-list',
  templateUrl: './user-grid-list.component.html',
  styleUrls: ['./user-grid-list.component.scss']
})
export class UserGridListComponent implements OnInit {

	userGridList : any [] = [
		{
			backgroundImage:"assets/img/gridlist-1.jpg",
			image : "assets/img/user-1.jpg",
			name : "Robert See",
			location : "Durham",
			colorClass :"primary",
			buttonColor: "primary",
			icon:"done"
		},
		{
			backgroundImage:"assets/img/gridlist-2.jpg",
			image : "assets/img/user-2.jpg",
			name : "Alice Perrone",
			location : "San Diego",
			colorClass :"warn",
			buttonColor: "accent",
			icon:"clear"

		},
		{
			backgroundImage:"assets/img/gridlist-3.jpg",
			image : "assets/img/user-3.jpg",
			name : "Daniel Bagley",
			location : "Morden",
			colorClass :"primary",
			buttonColor: "primary",
			icon:"done"
		},
		{
			backgroundImage:"assets/img/gridlist-4.jpg",
			image : "assets/img/user-4.jpg",
			name : "Carolyn Copeland",
			location : "Hartford",
			colorClass :"warn",
			buttonColor: "accent",
			icon:"clear"
		},
		{
			backgroundImage:"assets/img/gridlist-5.jpg",
			image : "assets/img/user-5.jpg",
			name : "Annie Workman",
			location : "Houston",
			colorClass :"primary",
			buttonColor: "primary",
			icon:"done"
		},
		{
			backgroundImage:"assets/img/gridlist-6.jpg",
			image : "assets/img/user-6.jpg",
			name : "Anna Robertson",
			location : "Owensboro",
			colorClass :"warn",
			buttonColor: "accent",
			icon:"clear"
		},
		{
			backgroundImage:"assets/img/gridlist-1.jpg",
			image : "assets/img/user-7.jpg",
			name : "Priscilla Hughes",
			location : "Houston",
			colorClass :"primary",
			buttonColor: "primary",
			icon:"done"
		},
		{
			backgroundImage:"assets/img/gridlist-2.jpg",
			image : "assets/img/user-8.jpg",
			name : "Flossie Morrow",
			location : "Brock",
			colorClass :"warn",
			buttonColor: "accent",
			icon:"clear"
		},
		{
			backgroundImage:"assets/img/gridlist-3.jpg",
			image : "assets/img/user-9.jpg",
			name : "Joseph Prasad",
			location : "Toronto",
			colorClass :"primary",
			buttonColor: "primary",
			icon:"done"
		},
		{
			backgroundImage:"assets/img/gridlist-4.jpg",
			image : "assets/img/user-9.jpg",
			name : "Ronald Lee",
			location : "Erin Mills",
			colorClass :"primary",
			buttonColor: "primary",
			icon:"done"
		},
		{
			backgroundImage:"assets/img/gridlist-5.jpg",
			image : "assets/img/user-10.jpg",
			name : "Charles Numbers",
			location : "Surrey",
			colorClass :"warn",
			buttonColor: "accent",
			icon:"clear"
		},
		{
			backgroundImage:"assets/img/gridlist-6.jpg",
			image : "assets/img/user-11.jpg",
			name : "Ron Mingo",
			location : "Montreal",
			colorClass :"primary",
			buttonColor: "primary",
			icon:"done"
		}
	]

	constructor(private pageTItleService : PageTitleService) { }

	ngOnInit() {
		this.pageTItleService.setTitle("User Grid List")
	}

}
