import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { SortablejsOptions} from "angular-sortablejs";
import { fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'ms-sortable',
	templateUrl:'./sortable-component.html',
	styleUrls: ['./sortable-component.scss'],
	encapsulation: ViewEncapsulation.None,
	host: {
		"[@fadeInAnimation]": 'true'
	},
	animations: [ fadeInAnimation ]
})

export class SortableDemoComponent implements OnInit {

	list1        : any[];
	list2        : any[];
	numbers      : any[];

	groupOptions : SortablejsOptions = {
		group     : 'testGroup',
		handle    : '.drag-handle',
		animation : 300
	};

	simpleOptions : SortablejsOptions = {
		animation : 300
	};

	constructor(private pageTitleService: PageTitleService, private translate : TranslateService) {}

	ngOnInit() {
		 
		this.pageTitleService.setTitle("SortableJS");

		this.list1 = [
			{
				image: 'assets/img/user-1.jpg',
				name: 'Dennis',
				subject: 'Project',
				content: 'Need help to setup the project..!'
			},
			{
				image: 'assets/img/user-2.jpg',
				name: 'Harry',
				subject: 'Birthday',
				content: 'See You there!'
			},
			{
				image: 'assets/img/user-3.jpg',
				name: 'Peter',
				subject: 'Rating',
				content: 'You Guys are awesome ,Love U!'
			},
			{
				image: 'assets/img/user-4.jpg',
				name: 'Bella',
				subject: 'Exam',
				content: 'Hope you prepare for the exam'
			},
			{
				image: 'assets/img/user-1.jpg',
				name: 'Joy',
				subject: 'Issue',
				content: 'Can not connect to my ip address'
			}
		];

		this.list2 = [
			{
				name: 'Louis Jordan'
			},
			{
				name: 'Michael Ward'
			},
			{
				name: 'Lois Freeman'
			},
			 {
				name: 'Anna Alexander'
			},
	        {
				name: 'Noris Lewis'
			},
			{
				name: 'Vicky Smith'
			},
			{
				name: 'Anna Michel'
			}
		];

		this.numbers = [
			{
				name: 'Anne Boyd'
			},
			{
				name: 'Daniel Myers'
			},
			{
				name: 'William Price'
			},
			{
				name: 'Katherine Alexander'
			},
	        {
				name: 'Kate Golmes'
			},
			{
				name: 'Denna Michel'
			},
			{
				name: 'Steve White'
			}
		];
	}
}


