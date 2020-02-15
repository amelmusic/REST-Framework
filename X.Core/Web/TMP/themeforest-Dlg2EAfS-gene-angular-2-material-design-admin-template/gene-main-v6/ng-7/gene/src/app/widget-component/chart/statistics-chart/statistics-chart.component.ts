import { Component, OnInit, Input } from '@angular/core';

@Component({
   selector: 'ms-statistics',
   templateUrl: './statistics-chart.component.html',
   styleUrls: ['./statistics-chart.component.scss']
})

export class StatisticsComponent implements OnInit {

   @Input() label :any;
   @Input() data  :any;

   //line chart options
	public lineChartOptions :any = {
      responsive: true,
      maintainAspectRatio: false,
		scales: {
         yAxes: [{
            gridLines: {
              display: true,
              drawBorder: false
            },
            scaleLabel: {
               display: true,
               labelString: 'Cost'
            },
            ticks: {
              stepSize: 50
            }
          }],
         xAxes: [{
            gridLines: {
              display: false,
              drawBorder: false
            },
            scaleLabel: {
               display: true,
               labelString: 'Time'
            },
         }]
      },
		tooltip: {
			enabled: true
		},
		legend: {
			display: false
		},
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
