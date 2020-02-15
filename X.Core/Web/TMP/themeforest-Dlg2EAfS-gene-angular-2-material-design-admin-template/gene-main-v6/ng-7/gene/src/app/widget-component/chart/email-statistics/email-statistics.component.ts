import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-email-statistics',
	templateUrl: './email-statistics.component.html',
	styleUrls: ['./email-statistics.component.scss']
})
export class EmailStatisticsComponent implements OnInit {

	@Input() doughnutChartData : any;
	@Input() doughnutChartLabels : any;
	@Input() doughnutChartOptions : any;
	@Input() doughnutChartColors : any;
	@Input() doughnutChartType : any;
	showChart : boolean;
	
	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}

}