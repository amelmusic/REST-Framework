import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {pieChartDemoData, lineChartDemoDataGenerator} from "../../data/widgetDemoData.data";
import { stackedAreaChartData } from "../../data/stackedAreaChart.data";
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';
import { CoreService } from '../../service/core/core.service';

@Component({
   selector: 'ms-dashboard1',
   templateUrl:'./webanalytics-component.html',
   styleUrls: ['./webanalytics-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class WebAnalyticsComponent implements OnInit  {
	
  pieChartDemoData;
  lineChartDemoData;
  stackedAreaChartOptions;
  stackedAreaChartData;
  rows = [];

  saleCard : any [] = [
    {
      flex : "start",
      title : "Quality Sold",
      status : "-2% Than last year",
      sold_out : "4216/10,540",
      percent : "40",
      text_color : "warn-text",
      options: {
        barColor: '#E53935',
        trackColor: '#f0f2f9',
        scaleColor: '#dfe0e0',
        scaleLength: 0,
        lineCap: 'round',
        lineWidth: 3,
        size: 125,
        rotate: 0,
        animate: {
          duration: 3000,
          enabled: true
        }
      }
    },
    {
      flex : "end",
      title : "Grow Sales",
      status : "+2.5% Than last year",
      price : "$45,815",
      percent : "68",
      options: {
        barColor: '#66BB6A',
        trackColor: '#f0f2f9',
        scaleColor: '#dfe0e0',
        scaleLength: 0,
        lineCap: 'round',
        lineWidth: 3,
        size: 125,
        rotate: 0,
        animate: {
          duration: 3000,
          enabled: true
        }
      }
    }
  ]

  //social card content 
  socialCardContent : any[] = [
    {
      name : "Facebook",
      text_color : "primary-text",
      icon : "fa-facebook",
      budget : "4650",
      growth : "2.40"
    },
    {
      name : "Google +",
      text_color : "success-text",
      icon : "fa-google-plus",
      budget : "2875",
      growth : "1.50"
    },
    {
      name : "Twitter",
      text_color : "accent-text",
      icon : "fa-twitter",
      budget : "1024",
      growth : "1.08"
    },
    {
      name : "Youtube",
      text_color : "warn-text",
      icon : "fa-youtube",
      budget : "4047",
      growth : "3.88"
    }
  ]  

  //server card content 
  serverCardContent : any [] = [
    {
      title : "Server AntiTheft",
      validity : "24/Mo",
      icon : "fa-shield",
      validity_color : "primary-bg",
      content : "We provide full insurance against any aspect of Server Secuity.We provide better solution for server"
    },
    {
      title : "24*7 Assistance",
      validity : "12/Mo",
      icon : "fa-life-ring",
      content : "We always here to help you anytime.Contact us on our customer care helpline for more information",
      validity_color : "accent-bg"
    },
    {
      title : "NextGen Server",
      validity : "18/Mo",
      icon : "fa-server",
      content : "We provide 3rd generation server for your site.Which provide good speed in data processing and security",
      validity_color : "warn-bg"
    },
    {
      title : "Cloud Pro",
      validity : "14/Mo",
      icon : "fa-sellsy",
      content : "For backup, we provide cloud storage which is highly secure and you can access data anytime and everywhere",
      validity_color : "success-bg"
    }
  ]

  // Browser Stats
   public barStackChartOptions:any = {
      scaleShowVerticalLines: false,
      responsive: true,
      scales: {
         xAxes: [{
            stacked: false,
            barThickness: 35,
            display: true,
            gridLines: {
               color: 'rgba(0,0,0,0)',
               zeroLineColor: 'rgba(0,0,0,0)'
            }
         }],
         yAxes: [{
            stacked: true,
            barThickness: 35,
            gridLines: {
               color: 'rgba(0, 0, 0, 0.2)',
               zeroLineColor: 'rgba(0, 0, 0, 0.2)'
            }
         }]
      }
   };
   public barChartLabels:string[] = ['Safari', 'Chrome', 'Opera', 'IE+', 'Firefox'];
   public barChartType:string = 'bar';
   public barChartLegend:boolean = false;
   public barChartData:any[] = [
      {data: [65, 59, 80, 81, 56], label: 'Series A'},
      {data: [28, 48, 40, 19, 86], label: 'Series B'}
   ];

   barChartColors: Array <any> = [{
      backgroundColor: 'rgba(59, 85, 230, 1)',
      borderColor: 'rgba(59, 85, 230, 1)',
      pointBackgroundColor: 'rgba(59, 85, 230, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(59, 85, 230, 1)'
   }, {
      backgroundColor: 'rgba(240, 242, 247, 1)',
      borderColor: 'rgba(240, 242, 247, 1)',
      pointBackgroundColor: 'rgba(240, 242, 247, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(240, 242, 247, 1)'
   }];

  //Simple Bar Chart
   public simpleBarChartLabels:string[] = ['Jan', 'Feb', 'Mar', 'Apr', 'May'];
   public simpleBarChartLegend:boolean = true;
   public simpleBarChartData:any[] = [
      {data: [65, 59, 80, 81, 56, 55, 40], label: 'Purchase'},
      {data: [28, 48, 40, 19, 86, 27, 90], label: 'Plans'},
      {data: [18, 38, 20, 9, 66, 37, 70], label: 'Services'}
   ];
   public simpleBarStackChartOptions:any = {
      scaleShowVerticalLines: false,
      responsive: true,
      scales: {
         xAxes: [{
            stacked: true,
         }],
         yAxes: [{
            stacked: true,
         }]
      }
   };

   // PolarArea
   public polarAreaChartLabels:string[] = ['Download Sales', 'In-Store Sales', 'Mail Sales'];
   public polarAreaChartData:number[] = [300, 150, 350];
   public polarAreaLegend:boolean = true;
   public polarAreaChartType:string = 'polarArea';
   polarChartColors: any[] = [{
      backgroundColor: [ 'rgba(67, 210, 158, 0.8)','rgba(59, 85, 230, 0.8)', 'rgba(235, 78, 54, 0.8)']
   }];

   //Mixed Chart
   public mixedChartLabels:Array<any> = ['1', '2', '3', '4', '5', '6', '7'];
   public mixedChartLegend:boolean = false;
   mixedPointChartData: Array <any> = [{
      data: [6, 5, 8, 8, 5, 5, 4],
      label: 'Series A',
      borderWidth: 3,
      type: 'line',
      fill: false
   }, {
      data: [5, 4, 5, 2, 6, 3, 5],
      label: 'Series B',
      borderWidth: 1,
      type: 'bar'
   }];
   mixedChartColors: Array <any> = [{
      backgroundColor: 'rgba(67, 210, 158, 1)',
      borderColor: 'rgba(67, 210, 158, 1)',
      pointBackgroundColor: 'rgba(67, 210, 158, 1)',
      pointBorderColor: 'rgba(67, 210, 158, 1)',
      pointHoverBackgroundColor: 'rgba(67, 210, 158, 1)',
      pointHoverBorderColor: 'rgba(67, 210, 158, 1)'
   }, {
      backgroundColor: 'rgba(240, 242, 247, 1)',
      borderColor: 'rgba(240, 242, 247, 1)',
      pointBackgroundColor: 'rgba(240, 242, 247, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(240, 242, 247, 1)'
   }];
   mixedChartOptions: any = {
      animation: false,
      scales: {
         yAxes: [{
            ticks: {
               beginAtZero: true,
               suggestedMax: 9,
            }
         }]
      },
      responsive: true,
   };

   //User Activities
   activities: Object[] = [{
      title: "Benn Holmes",
      photo: "assets/img/user-1.jpg",
      desc: "Changed his profile and last name.",
      activitydate: "25 min ago"
   },{
      title: "Smith Collin",
      photo: "assets/img/user-2.jpg",
      desc: "Update the infomation regarding project 2.",
      activitydate: "19 min ago"
   },{
      title: "Ellen Wood",
      photo: "assets/img/user-3.jpg",
      desc: "Updated 3 images from the post.",
      activitydate: "18 min ago"
   },{
      title: "Gorrel Haze",
      photo: "assets/img/user-11.jpg",
      desc: "Delete Trash folder from the cloud storage.",
      activitydate: "18 min ago"
   },{
      title: "Hema Mills",
      photo: "assets/img/user-9.jpg",
      desc: "Install the Project 2 and remove previous one.",
      activitydate: "3 min ago"
   }
   ,{
      title: "Mr. Terry",
      photo: "assets/img/user-7.jpg",
      desc: "Changed and resize site logo for Old Projects.",
      activitydate: "1 min ago"
   }];

   //Messages
   message: Object[] = [{
      title: "Phil Fordy",
      photo: "assets/img/user-1.jpg",
      desc: "Thanks Guys! You Save our time. Highly Impressed with you guys.",
      msgdate: "15 min ago"
   },{
      title: "Smith",
      photo: "assets/img/user-2.jpg",
      desc: "Happy with your support and product Well done guys.Best of Luck. ",
      msgdate: "10 min ago"
   },{
      title: "Mccullam",
      photo: "assets/img/user-3.jpg",
      desc: "Nice work! Remarkable product and support.Thanks for Quick Response.",
      msgdate: "8 min ago"
   },
   {
      title: "Billy Wales",
      photo: "assets/img/user-6.jpg",
      desc: "This is the theme am looking for long time.Smooth, simple and light.",
      msgdate: "5 min ago"
   },
   {
      title: "Harry Smith",
      photo: "assets/img/user-5.jpg",
      desc: "Search ends here! Waiting for long time.Good Luck.",
      msgdate: "3 min ago"
   },
   {
      title: "David Brown",
      photo: "assets/img/user-4.jpg",
      desc: "Happy with customization of the theme ,Good Product With Good design.",
      msgdate: "2 min ago"
   }];

   //Tickets
   tickets: Object[] = [{
      title: "Johh Rims",
      photo: "assets/img/register-user-1.jpg",
      desc: "Facebook link not open.",
      status: "Closed"
   },{
      title: "Smith Collin",
      photo: "assets/img/register-user-2.jpg",
      desc: "Notice and warning in Site 2 ",
      status: "Progress"
   },
   {
      title: "Terry Blazes",
      photo: "assets/img/register-user-4.jpg",
      desc: "Unable to find updated images.",
      status: "Open"
   },{
      title: "Mccullam Ted",
      photo: "assets/img/user-9.jpg",
      desc: "Problem In buying plans.",
      status: "Progress"
   },
   {
      title: "Mr. White",
      photo: "assets/img/user-10.jpg",
      desc: "Installation Setup not processed fully.",
      status: "Progress"
   },{
      title: "David Vuen",
      photo: "assets/img/user-11.jpg",
      desc: "Problem In popup in Chrome",
      status: "Open"
   }];

  //Chat
   chats: Object[] = [{
      title: "Maria Holmes",
      photo: "assets/img/user-8.jpg",
      desc: "Where I found documention of this theme and in documentation installation process is mentioned or not?",
      chatdate: "7 min ago",
      admin: true
   },{
      title: "Kaity Linn",
      photo: "assets/img/user-11.jpg",
      desc: "Hi Maria.Thanks for buying our product.Yes,We provided full documentation along with installation Steps.",
      chatdate: "5 min ago",
      admin: false
   },{
      title: "Maria Holmes",
      photo: "assets/img/user-8.jpg",
      desc: "Ya I found this under documentation folder.Thanks for your quick response.",
      chatdate: "3 min ago",
      admin: true
   },{
      title: "Kaity Linn",
      photo: "assets/img/user-11.jpg",
      desc: "We are always here to help you. You may reach us anytime. ",
      chatdate: "2 min ago",
      admin: false
   }];

   //weather Information
   "weatherInfo" : any [] = [
      {
         "day"  : "Monday",
         "date" : "8th May 2017",
         "time" : "12:26 PM",
         "image" : "assets/img/rain-snow.png",
         "temperature" : "32F",
         "wind" : "16km/h",
         "sunrise" : "07:15",
         "Humanfeel" : "32F",
         "sunset" : "21",
         "pressure" : "22In",
         "featureDayInfo" : [
            {
               "day" : "Tue",
               "weatherImage" : "assets/img/hail.png"
            },
            {
               "day" : "Wed",
               "weatherImage" : "assets/img/hail.png"
            },
            {
               "day" : "Thu",
               "weatherImage" : "assets/img/partly-cloudy.png"
            },
            {
               "day" : "Fri",
               "weatherImage" : "assets/img/hail.png"
            },
            {
               "day" : "Sat",
               "weatherImage" : "assets/img/partly-sunny.png"
            },
            {
               "day" : "Sun",
               "weatherImage" : "assets/img/hail.png"
            },
            {
               "day" : "Mon",
               "weatherImage" : "assets/img/hail.png"
            }
         ]
      }
   ]

   constructor(private pageTitleService: PageTitleService,
               private translate : TranslateService,
               private service : CoreService) {
   }

  ngOnInit() {
    this.pageTitleService.setTitle("Web Analytics");
    this.lineChartDemoData = lineChartDemoDataGenerator();
    this.pieChartDemoData = pieChartDemoData;
    this.stackedAreaChartData = stackedAreaChartData;
    this.stackedAreaChartOptions = {
      name: 'Sample Full Width Graph',
    };
  }
}
