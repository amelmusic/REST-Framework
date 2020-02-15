import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-project-status-chart',
  templateUrl: './project-status-chart.component.html',
  styleUrls: ['./project-status-chart.component.scss']
})
export class ProjectStatusChartComponent implements OnInit {

	@Input() color : any;
	@Input() label : any;
	@Input() data  : any;

	showChart : boolean = false;

	public barChartOptions :any = {
		responsive: true,
		maintainAspectRatio: false,
      scales: {
			xAxes: [{
				display: true,
					gridLines: {
					display: false,
					drawBorder: true,
				}
			}],
			yAxes: [{
				display: true,
				ticks: {
					display: true,
					stepSize: 200,
					beginAtZero : true,
	            min: 0,
	            max: 1600
				},
				gridLines: {
					display: true,
					drawBorder: false,
				}  
			}]
		},
		legend: {
			display: false
		}
   }

	constructor() { }

	ngOnInit() {
		setTimeout(()=>{
			this.showChart = true;
		},0)
	}

}
