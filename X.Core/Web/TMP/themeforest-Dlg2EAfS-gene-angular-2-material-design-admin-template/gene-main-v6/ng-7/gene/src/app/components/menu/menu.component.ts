import { Component, OnInit,ViewEncapsulation }      from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService} from '@ngx-translate/core';

@Component({
	selector: 'ms-menu',
  	templateUrl:'./menu-material.html',
  	styleUrls: ['./menu-material.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class MenuOverviewComponent implements OnInit{ 

	items = [{
	   text: 'Refresh'
	}, {
	   text: 'Settings'
	}, {
	   text: 'Help',
	   disabled: true
	}, {
	   text: 'Sign Out'
	}];

	iconItems = [{
	   text: 'Redial',
	   icon: 'dialpad'
	}, {
	   text: 'Check voicemail',
	   icon: 'voicemail',
	   disabled: true
	}, {
	   text: 'Disable alerts',
	   icon: 'notifications_off'
	}];

	constructor(private pageTitleService: PageTitleService,
					public translate : TranslateService) {}

	ngOnInit() {
		this.pageTitleService.setTitle("Menu");
	}
}


