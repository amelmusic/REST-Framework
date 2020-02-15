import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-exchange-statistics',
	templateUrl: './exchange-statistics.component.html',
	styleUrls: ['./exchange-statistics.component.scss']
})
export class ExchangeStatisticsComponent implements OnInit {

	@Input() exchangeStatistic : any;
	@Input() exchangeStatisticConfig : any;
	
	constructor() { }

	ngOnInit() {
	}

}
