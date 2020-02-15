import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-social-card-v2',
	templateUrl: './social-card-v2.component.html',
	styleUrls: ['./social-card-v2.component.scss']
})
export class SocialCardV2Component implements OnInit {

	@Input() socialCardContent : any;
	
	constructor() { }

	ngOnInit() {
	}

}
