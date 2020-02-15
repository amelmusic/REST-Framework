import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-email-statistics',
	templateUrl: './email-statistics.component.html',
	styleUrls: ['./email-statistics.component.scss']
})
export class EmailStatisticsComponent implements OnInit {

	@Input() doughnutChartData : any;
	@Input() doughnutChartLabels : any;
	@Input() doughnutChartColors : any;

	showChart : boolean;
	
	public doughnutChartOptions: any = {
      responsive: true,
      elements: {
         arc: {
            borderWidth: 0
         }
      },
      legend: {
         position: 'bottom',
         labels: {
            usePointStyle: true,
         }
      }
   }

	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}

}