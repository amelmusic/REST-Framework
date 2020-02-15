import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-buy-sell-chart',
	templateUrl: './buy-sell-chart.component.html',
	styleUrls: ['./buy-sell-chart.component.scss']
})

export class BuySellChartComponent implements OnInit {

	@Input() color :any;
   @Input() label :any;
   @Input() data  :any;
   showChart : boolean;
   //line chart options
	public lineChartOptions :any = {
      responsive: true,
      maintainAspectRatio: false,
		scales: {
         yAxes: [{
            gridLines: {
              display: true,
              drawBorder: false
            }
          }],
         xAxes: [{
            gridLines: {
              display: false,
              drawBorder: false
            }
         }]
      },
		tooltip: {
			enabled: true
		},
		legend: {
			display: false
		},
	}

	constructor() { }

	ngOnInit() {	
      setTimeout(()=>{
         this.showChart = true;
      },0)
	}
}
