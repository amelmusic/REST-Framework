import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-mixed-chart',
  templateUrl: './mixed-chart.component.html',
  styleUrls: ['./mixed-chart.component.scss']
})
export class MixedChartComponent implements OnInit {

	@Input() barChartType : any;
	@Input() mixedChartLegend : any;
	@Input() mixedChartColors : any;
	@Input() mixedChartOptions : any;
	@Input() mixedChartLabels : any;
	@Input() mixedPointChartData : any;

	showChart  : boolean;

	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}
}
