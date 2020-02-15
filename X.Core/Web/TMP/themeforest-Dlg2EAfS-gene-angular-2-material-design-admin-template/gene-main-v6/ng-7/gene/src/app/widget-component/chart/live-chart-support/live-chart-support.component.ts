import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-live-chart-support',
  templateUrl: './live-chart-support.component.html',
  styleUrls: ['./live-chart-support.component.scss']
})
export class LiveChartSupportComponent implements OnInit {

	@Input() polarAreaChartData : any;
	@Input() polarAreaChartLabels : any;
	@Input() polarChartColors : any;
	@Input() polarAreaChartType : any;
	showChart  : boolean;

	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}

}
