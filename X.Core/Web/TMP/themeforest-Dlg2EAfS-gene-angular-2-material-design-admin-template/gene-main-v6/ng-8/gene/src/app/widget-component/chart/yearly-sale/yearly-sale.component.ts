import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-yearly-sale',
  templateUrl: './yearly-sale.component.html',
  styleUrls: ['./yearly-sale.component.scss']
})
export class YearlySaleComponent implements OnInit {
	
	showChart : boolean ;
	@Input() barChartData : any ;
	@Input() barChartLabels : any ;
	@Input() barStackChartOptions : any ;
	@Input() barChartColors : any ;
	@Input() barChartLegend : any ;
	@Input() barChartType : any ;
	
	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}

}
