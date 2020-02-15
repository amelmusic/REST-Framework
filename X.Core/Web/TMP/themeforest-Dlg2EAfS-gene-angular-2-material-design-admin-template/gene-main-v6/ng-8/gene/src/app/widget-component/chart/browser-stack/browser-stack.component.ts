import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-browser-stack',
  templateUrl: './browser-stack.component.html',
  styleUrls: ['./browser-stack.component.scss']
})
export class BrowserStackComponent implements OnInit {
	
	@Input() barChartData : any;
	@Input() barChartLabels : any;
	@Input() barStackChartOptions : any;
	@Input() barChartColors : any;
	@Input() barChartLegend : any;
	@Input() barChartType : any;
	showChart  : boolean;

	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}
}
