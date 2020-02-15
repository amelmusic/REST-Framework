import { Component, OnInit,NgZone, Input } from '@angular/core';

@Component({
	selector: 'ms-market-cap-charts',
	templateUrl: './market-cap-charts.component.html',
	styleUrls: ['./market-cap-charts.component.scss']
})
export class MarketCapChartsComponent implements OnInit {

	@Input() label :any;
   @Input() data  :any;

   //line chart options
	public lineChartOptions :any = {
      maintainAspectRatio: false,
		responsive: true,
		scales: {
         yAxes: [{
            margin : 50,
            gridLines: {
              display: true,
              drawBorder: false
            },
            scaleLabel: {
               display: true,
               labelString: 'Cost'
            }
         }],
         xAxes: [{
            gridLines: {
              display: true,
              drawBorder: false
            },
            scaleLabel: {
               display: true,
               labelString: 'Timeline'
            },
         }]
      },
		tooltip: {
			enabled: true
		},
		legend: {
			display: false
		}
	}

   //line chart color
   public color: Array <any> = [
      {
         lineTension: 0.4,
         borderColor: '#1565c0',
         pointBorderColor: '#1565c0',
         pointBorderWidth: 2,
         pointRadius: 7,
         fill: false,
         pointBackgroundColor: '#FFFFFF',
         borderWidth: 3
      }
   ];

   constructor() { }

   ngOnInit() {
   }

}
