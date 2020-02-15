import { Component, OnInit,ViewEncapsulation }      from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'ms-list',
	templateUrl:'./list-material.html',
	styleUrls: ['./list-material.scss'],
	encapsulation: ViewEncapsulation.None,
	host: {
		"[@fadeInAnimation]": 'true'
	},
	animations: [ fadeInAnimation ]
})

export class ListOverviewComponent implements OnInit{

	items: string[] = ['Item 1', 'Item 2', 'Item 3'];

	messages: any[] = [{
	   from: 'Susan',
	   subject: 'Sturday Plan',
	   message: 'As we discussed yesterday.Are you ready for going?',
	   image: 'assets/img/user-9.jpg'
	}, {
	   from: 'Rose',
	   subject: 'Sure',
	   message: 'Yes,I packed my bag.I am very Excited.',
	   image: 'assets/img/user-8.jpg'
	}, {
	   from: 'Katy',
	   subject: 'Fun Day',
	   message: 'Lets do some exciting things on sturday.Come on!',
	   image: 'assets/img/user-11.jpg'
	}];

	constructor( private pageTitleService: PageTitleService, 
					 private translate : TranslateService) {}

	ngOnInit() {
 		this.pageTitleService.setTitle("List");
	}

}


