import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-full-width-graph',
	templateUrl: './full-width-graph.component.html',
	styleUrls: ['./full-width-graph.component.scss']
})

export class FullWidthGraphComponent implements OnInit {
	
	@Input() lineChartData : any;
	@Input() lineChartLabels : any;
	@Input() lineChartOptions : any;
	@Input() lineChartColors : any;
	@Input() lineChartLegend : any;
	@Input() lineChartType : any;
	showChart : boolean ;

	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}

}
