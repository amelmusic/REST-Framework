import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-charts',
   templateUrl:'./chart-component.html',
   styleUrls: ['./chart-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class ChartComponent implements OnInit {

   showChart : boolean;
  
   /*
      ---------- Bar Chart ----------
   */

   public barChartLabels:string[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
   public barChartType:string = 'bar';
   public barChartLegend:boolean = false;

   public barChartData:any[] = [
      {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
      {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'}
   ];

   public barChartOptions : any = {
      scaleShowVerticalLines : false,
      responsive : true
   };

   barChartColors: Array <any> = [{
      backgroundColor: 'rgba(59, 85, 230, 1)',
      borderColor: 'rgba(59, 85, 230, 1)',
      pointBackgroundColor: 'rgba(59, 85, 230, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(59, 85, 230, 1)'
   }, {
   backgroundColor: 'rgba(235, 78, 54, 1)',
      borderColor: 'rgba(235, 78, 54, 1)',
      pointBackgroundColor: 'rgba(235, 78, 54, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(235, 78, 54, 1)'
   },{
   backgroundColor: 'rgba(67, 210, 158, 0.2)',
      borderColor: 'rgba(67, 210, 158, 1)',
      pointBackgroundColor: 'rgba(67, 210, 158, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(67, 210, 158, 0.8)'
   }];

   //Horizontal Bar
   public barHorizontalChartType:string = 'horizontalBar';
   public barHorizontalChartLegend:boolean = false;

   public barHorizontalChartOptions:any = {
      scaleShowVerticalLines: false,
      responsive: true
   };

   //Stacked Bar
   public barStackChartOptions:any = {
      scaleShowVerticalLines: false,
      responsive: true,
      scales: {
         xAxes: [{
            stacked: true,
         }],
         yAxes: [{
            stacked: true
         }]
      }
   };

   /*
      ---------- Line Chart ----------
   */
     
   public lineChartLabels:Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
   public lineChartOptions:any = {
      responsive: true
   };
   public lineChartLegend:boolean = false;
   public lineChartType:string = 'line';

   public lineChartData:Array<any> = [
      {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
      {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'},
      {data: [18, 48, 77, 9, 100, 27, 40], label: 'Series C'}
   ];

   lineChartColors: Array <any> = [{
      backgroundColor: 'rgba(59, 85, 230, 0.2)',
      borderColor: 'rgba(59, 85, 230, 1)',
      pointBackgroundColor: 'rgba(59, 85, 230, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(59, 85, 230, 0.8)'
   }, {
      backgroundColor: 'rgba(235, 78, 54, 0.2)',
      borderColor: 'rgba(235, 78, 54, 1)',
      pointBackgroundColor: 'rgba(235, 78, 54, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(235, 78, 54, 0.8)'
   },{
      backgroundColor: 'rgba(67, 210, 158, 0.2)',
      borderColor: 'rgba(67, 210, 158, 1)',
      pointBackgroundColor: 'rgba(67, 210, 158, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(67, 210, 158, 0.8)'
   }];

   //Stepped Line Chart
   lineChartSteppedData: Array <any> = [{
      data: [65, 59, 80, 81, 56, 55, 40],
      label: 'Series A',
      borderWidth: 1,
      fill: false,
      steppedLine: true
   },{
      data: [28, 48, 40, 19, 86, 27, 90],
      label: 'Series B',
      borderWidth: 1,
      fill: false,
      steppedLine: true
   },{
      data: [18, 48, 77, 9, 100, 27, 40],
      label: 'Series C',
      borderWidth: 1,
      fill: false,
      steppedLine: true
   }];

   //Point Chart
   linePointChartData: Array <any> = [{
      data: [65, 59, 80, 81, 56, 55, 40],
      label: 'Series A',
      borderWidth: 1,
      fill: false,
      pointRadius: 10,
      pointHoverRadius: 15,
      showLine: false
      },{
      data: [28, 48, 40, 19, 86, 27, 90],
      label: 'Series B',
      borderWidth: 1,
      fill: false,
      pointRadius: 10,
      pointHoverRadius: 15,
      showLine: false
      },{
      data: [18, 48, 77, 9, 100, 27, 40],
      label: 'Series C',
      borderWidth: 1,
      fill: false,
      pointRadius: 10,
      pointHoverRadius: 15,
      showLine: false
   }];

   linePointChartOptions: any = {
      elements: {
         point: {
            pointStyle: 'rectRot',
         }
      }
   };

   /*
      ---------- Pie Chart ----------
   */

   public pieChartData:number[] = [300, 500, 100];
   public pieChartType:string = 'pie';
   pieChartColors: any[] = [{
      backgroundColor: ['#3B55E6', '#EB4E36', '#43D29E', '#32CBD8', '#E8C63B']
   }];
   PieChartOptions: any = {
      elements: {
         arc: {
            borderWidth: 0
         }
      }
   }

   /*
      ---------- Doughnut Chart ----------
   */
   public doughnutChartData:number[] = [350, 450, 100];
   public doughnutChartType:string = 'doughnut';

   /*
      ----------Polar Area Chart ----------
   */
   public polarAreaChartLabels:string[] = ['Download Sales', 'In-Store Sales', 'Mail Sales', 'Telesales', 'Corporate Sales'];
   public polarAreaChartData:number[] = [300, 500, 100, 40, 120];
   public polarAreaLegend:boolean = false;
   public polarAreaChartType:string = 'polarArea';

   /*
      ---------- Radar Chart ----------
   */
   public radarChartLabels:string[] = ['Eating', 'Drinking', 'Sleeping', 'Designing', 'Coding', 'Cycling', 'Running'];
   public radarChartData:any = [
   {data: [65, 59, 90, 81, 56, 55, 40], label: 'Series A'},
   {data: [28, 48, 40, 19, 96, 27, 100], label: 'Series B'}
   ];
   public radarChartType:string = 'radar';

   /*
      ---------- Bubble Chart ----------
   */   
   bubbleChartData: Array <any> = [{
      data: [{
         x: 6,
         y: 5,
         r: 15,
      }, {
         x: 5,
         y: 4,
         r: 10,
      }, {
         x: 8,
         y: 4,
         r: 6,
      }, {
         x: 8,
         y: 4,
         r: 6,
      }, {
         x: 5,
         y: 14,
         r: 14,
      }, {
         x: 5,
         y: 6,
         r: 8,
      }, {
         x: 4,
         y: 2,
         r: 10,
      }],
      label: 'Series A',
      borderWidth: 1
   }];

   bubbleChartType = 'bubble';
   public bubbleChartOptions:any = {
      responsive: true,
      elements: {
         points: {
            borderWidth: 1,
            borderColor: 'rgb(0, 0, 0)'
         }
      }
   };

   /*
      ---------- Mixed Chart ----------
   */
   mixedPointChartData: Array <any> = [{
      data: [6, 5, 8, 8, 5, 5, 4],
      label: 'Series A',
      borderWidth: 1,
      type: 'line',
      fill: false
   }, {
      data: [5, 4, 4, 2, 6, 2, 5],
      label: 'Series B',
      borderWidth: 1,
      type: 'bar',
   }];

   mixedChartOptions: any = {
      responsive: true,
      scales: {
         xAxes: [{
            gridLines: {
               color: 'rgba(0,0,0,0.02)',
               zeroLineColor: 'rgba(0,0,0,0.02)'
            }
         }],
         yAxes: [{
            gridLines: {
              color: 'rgba(0,0,0,0.02)',
               zeroLineColor: 'rgba(0,0,0,0.02)'
            },
            ticks: {
               beginAtZero: true,
               suggestedMax: 9,
            }
         }]
      }
   };

   constructor(private pageTitleService: PageTitleService, private translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Ng2Charts");
      setTimeout(() =>{
         this.showChart = true;
      },0)
   }
	
  
}



