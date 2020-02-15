import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-stats-card',
	templateUrl: './stats-card.component.html',
	styleUrls: ['./stats-card.component.scss']
})
export class StatsCardComponent implements OnInit {
	
	@Input() statsCardData  : any;

	constructor() { }

	ngOnInit() {
	}

}
