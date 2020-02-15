import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-user-profile-v2',
  templateUrl: './user-profile-v2.component.html',
  styleUrls: ['./user-profile-v2.component.scss']
})
export class UserProfileV2Component implements OnInit {


	userProfile : any [] =  [
		{
			userImage  : "assets/img/user-200x200.jpg",
			userName   : "Stormy Estes (demo)",
			profession : "Professor",
			department : "Senior Group Leader And Professor of Surgical Oncology",
			place      : "University of Cambridge",
			postCount: "50",
			followersCount : "92",
			followingCount : "73",
			viewCount : "95"
		}
	]
	

	publicationArray : any [] = [
		{
			image : "/assets/img/user-1.jpg",
			heading : "Lorem ipsum dolor sit amet, consectetur adipisicing ut labore et dolore magna.",
			contentType : "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
			reference : "Learn All From This.",
			text : "Five Brilliant Ways To Advertise"
 		},
 		{
			image : "/assets/img/user-2.jpg",
			heading : "Consectetur adipisicing ut labore et dolore magna aliqua lorem ipsum dolor sit amet.",
			contentType : "Dolor in reprehenderit in voluptate velit esse cillum ullamco laboris nisi ut aliquip ex ea commodo",
			reference : "Learn All From This",
			text : "Five Brilliant Ways To Advertise"
 		},
 		{
			image : "/assets/img/user-3.jpg",
			heading : "Ullamco laboris nisi ut aliquip ex ea commodo consequat Duis aute irure dolor.",
			contentType : "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod ut labore et dolore magna aliqua.",
			reference : "Learn All From This",
			text : "Five Brilliant Ways To Advertise"
 		},
 		{
			image : "/assets/img/user-4.jpg",
			heading : "Duis aute irure dolor in reprehenderit in voluptate velit ess dolore eu Cancer.",
			contentType : "Exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat consectetur adipisicing elilaborum.",
			reference : "Learn All From This",
			text : "Five Brilliant Ways To Advertise"
 		},
 		{
			image : "assets/img/user-5.jpg",
			heading : "Lorem ipsum dolor sit amet, consectetur adipisicing ut labore et dolore magna aliqua.",
			contentType : "Adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua commodo consequat.",
			reference : "Learn All From This",
			text : "Five Brilliant Ways To Advertise"
 		}
	]

	researchInterest : any = [
		{
			type : "Health care",
			color : "primary"
		},
		{
			type : "Education planner",
			color : "accent"
		},
		{
			type : "Surgical Oncology",
			color : "warn"
		},
		{
			type : "Prostate Cancer",
			color : "accent"
		},
		{
			type : "Surgical",
			color : "primary"
		}
	]

	followers : any = [
		{
			image : "assets/img/user-1.jpg",
			name  : "Tony Freund",
			place : "Miami"
		},
		{
			image : "assets/img/user-2.jpg",
			name  : "Etta Spano",
			place : "Fayetteville"
		},
		{
			image : "assets/img/user-3.jpg",
			name  : "Archie Enriquez",
			place : "Rose Hill"
		},
		{
			image : "assets/img/user-4.jpg",
			name  : "Betty Rivera",
			place : "Houston"
		},
		{
			image : "assets/img/user-5.jpg",
			name  : "Tessa Hinrichs",
			place : "Los Angeles"
		},
		{
			image : "assets/img/user-6.jpg",
			name  : "Rena Blackmon",
			place : "Los Angeles"
		}
	]

	constructor( private pageTitleService : PageTitleService,private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("User Profile")
	}

}
