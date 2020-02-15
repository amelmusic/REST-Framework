import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-pricing1',
  templateUrl: './pricing1.component.html',
  styleUrls: ['./pricing1.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class Pricing1Component implements OnInit {

	checked : boolean = false;

	pricingFaq : any [] = [
		{
			heading : "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod.",
			content : "Sed do eiusmod tempor incididunt ut labore lorem ipsum dolor sit amet, consectetur adipisicing elit, et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse."
		},
		{
			heading : "Sed do eiusmod lorem ipsum dolor sit amet, consectetur adipisicing elit.",
			content : "Consectetur adipisicing elit lorem ipsum dolor sit amet, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse."
		},
		{
			heading : "Adipisicing ipsum dolor sit amet, consectetur, sed do eiusmod elitlorem.",
			content : "Sit amet, consectetur adipisicing elit lorem ipsum dolor, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse."
		},
		{
			heading : "Dolor sit amet ipsum, consectetur adipisicing elit, sed do eiusmod lorem.",
			content : "Sed do eiusmod tempor incididunt ut labore lorem ipsum dolor sit amet, consectetur adipisicing elit,et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse."
		},
		{
			heading : "Amet Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod.",
			content : "Reprehenderit Incididunt ut lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in voluptate velit esse in."
		},
		{
			heading : "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod.",
			content : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse lorem ipsum dolor sit amet."
		}
	]

	constructor(private pageTitleService: PageTitleService, private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Pricing-1");
	}

	//onChange method is checked slide toggle checked or unchecked.-
	onChange(event) {
		this.checked = event;
	}
}
