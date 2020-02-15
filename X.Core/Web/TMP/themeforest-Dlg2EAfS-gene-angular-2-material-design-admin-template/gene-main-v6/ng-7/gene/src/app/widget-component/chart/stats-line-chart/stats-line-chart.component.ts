import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-stats-line-chart',
  templateUrl: './stats-line-chart.component.html',
  styleUrls: ['./stats-line-chart.component.scss']
})
export class StatsLineChartComponent implements OnInit {

	@Input() data  : any;
	@Input() label : any;
	@Input() color : any;
   showChart : boolean;

   //line chart options
	public lineChartOptions :any = {
      responsive: true,
      maintainAspectRatio: false,
      animation: {
          duration: 0
      },
      scales: {
         yAxes: [{
            ticks: {
               beginAtZero: true,
               display: false
            },
            gridLines: {
               display: false,
               drawBorder: false,
               drawTicks: false
            },
            display: false
         }],
         xAxes: [{
            ticks: {
               display: false,
               beginAtZero: true
            },
            gridLines: {
               display: true,
               drawBorder: false
            },
            display: false
         }]
      },
      legend: {
         display: false
      },
      tooltips: {enabled: false},
      hover: {mode: null}
   }

	constructor() { }

	ngOnInit() {
      setTimeout(()=>{
         this.showChart = true;
      },1000)
	}

}
