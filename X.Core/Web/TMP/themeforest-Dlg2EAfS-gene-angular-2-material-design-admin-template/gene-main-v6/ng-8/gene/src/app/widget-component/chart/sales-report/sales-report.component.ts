import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-sales-report',
	templateUrl: './sales-report.component.html',
	styleUrls: ['./sales-report.component.scss']
})

export class SalesReportComponent implements OnInit {

	@Input() bubbleChartData : any;
	@Input() lineChartLabels : any;
	@Input() bubbleChartOptions : any;
	@Input() bubbleChartColors : any;
	@Input() lineChartLegend : any;
	@Input() bubbleChartType : any;
	showChart  : boolean;

	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}

}
