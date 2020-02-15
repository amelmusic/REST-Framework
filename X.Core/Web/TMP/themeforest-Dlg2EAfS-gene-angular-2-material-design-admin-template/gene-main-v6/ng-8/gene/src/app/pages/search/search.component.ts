import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

	searchPage  : any []=  [
	  	{	
	  		image: "assets/img/event1-700x450.jpg",
	  		heading: "Now Is The Time For You To Know The Truth About",
	  		date: "11/12/2017",
	  		name:"Karen Coffee",
	  		content: "Consectetur adipisicing elit lorem ipsum dolor sit amet, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex quis nostrud exercitation ullamco laboris nisi."
	  	},
	  	{	
	  		image: "assets/img/event2-700x450.jpg",
	  		heading: "7 Advices That You Must Listen Before Studying",
	  		date: "02/02/2018",
	  		name:"Irene Propst",
	  		content: "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. lorem ipsum dolor sit amet, consectetur adipisicing elit, Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi quis nostrud exercitation ullamco laboris nisi ut aliquip ex ."
	  	},
	  	{	
	  		image: "assets/img/event3-700x450.jpg",
	  		heading: "Five Awesome Things You Can Learn From Studying",
	  		date: "08/05/2018",
	  		name:"Brenda Mendoza",
	  		content: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex. lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua quis nostrud exercitation ullamco laboris nisi."
	  	},
	  	{	
	  		image: "assets/img/event1-700x450.jpg",
	  		heading: "What You Know About And What You Don't Know About",
	  		date: "11/05/2018",
	  		name:"Linda Oneal",
	  		content: "labore et dolore magna aliqua. lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut quis nostrud exercitation ullamco laboris nisi aliquip ex."
	  	},
	  	{	
	  		image: "assets/img/event2-700x450.jpg",
	  		heading: "I Will Tell You The Truth About In The Next 60 Seconds",
	  		date: "10/11/2018",
	  		name:"Robert Ryan",
	  		content: "consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, lorem ipsum dolor sit amet,  quis nostrud exercitation ullamco laboris nisi ut aliquip ex quis nostrud exercitation ullamco laboris nisi."
	  	}
   ]

	constructor(private pageTitleService: PageTitleService, private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Search");
	}

}


