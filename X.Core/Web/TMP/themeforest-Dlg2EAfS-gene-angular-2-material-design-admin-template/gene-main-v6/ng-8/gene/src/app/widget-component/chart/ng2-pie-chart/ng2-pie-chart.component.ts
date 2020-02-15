import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-ng2-pie-chart',
	templateUrl: './ng2-pie-chart.component.html',
	styleUrls: ['./ng2-pie-chart.component.scss']
})
export class Ng2PieChartComponent implements OnInit {

	@Input() options : any;
	@Input() data : any;
	@Input() chartType : any;
	@Input() colors : any;
	@Input() labels  : any;

	constructor() { }

	ngOnInit() {
	}

}
