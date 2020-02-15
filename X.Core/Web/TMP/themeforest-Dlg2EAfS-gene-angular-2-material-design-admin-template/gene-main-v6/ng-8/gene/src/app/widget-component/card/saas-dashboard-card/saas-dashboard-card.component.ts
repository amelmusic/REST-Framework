import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-saas-dashboard-card',
  templateUrl: './saas-dashboard-card.component.html',
  styleUrls: ['./saas-dashboard-card.component.scss']
})

export class SaasDashboardCardComponent implements OnInit {

	@Input() statCard : any;
	
	constructor() { }

	ngOnInit() {
	}

}
