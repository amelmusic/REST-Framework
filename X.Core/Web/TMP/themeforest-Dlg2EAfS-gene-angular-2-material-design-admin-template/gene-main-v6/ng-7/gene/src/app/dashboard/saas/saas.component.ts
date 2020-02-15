import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';
import { CoreService } from '../../service/core/core.service';

@Component({
   selector: 'ms-dashboard',
   templateUrl:'./saas-component.html',
   styleUrls: ['./saas-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class SaasComponent implements OnInit  {

  rows = [];
  lineChartDemoData;
  newTodo              : string;
  todoObj              : any;
  lat                  : number = 50.937531;
  lng                  : number = 6.960278600000038;
  popUpDeleteResponse  : any;

  //stat card content
  statsCard : any [] = [
    {
      card_color : "primary-bg",
      title : "Today Views",
      number : "22,520",
      icon : "remove_red_eye",
      tourAnchor : "tour-ui"
    },
    {
      card_color : "warn-bg",
      title : "Revenue",
      number : "1,425",
      icon : "attach_money"
    },
    {
      card_color : "accent-bg",
      title : "Item Sales",
      number : "6,101",
      icon : "assessment"
    },
    {
      card_color : "success-bg",
      title : "New Orders",
      number : "5,218",
      icon : "new_releases"
    }
  ]

  //live chat support content
  liveChatSupport : any = [
    {
      image : "assets/img/register-user-3.jpg",
      name : "Devy Finn",
      chat : "Hi There! Recently I updated the latest version of your app, it crashed every time when i open.Please help me out as soon as possible.....Thanks",
      time : "10 Min ago",
      classSendBy : "sender"
    },
    {
       image : "assets/img/register-user-1.jpg",
       name : "Sam Brown",
       chat : "Hi Devy, Can you please tell us your mobile configuraion.So that We can help you better.Please Also specify Version of your phone....Thank You!",
       time : "8 Min ago",
       classSendBy : "receiver"
    },
    {
      image : "assets/img/register-user-3.jpg",
      name : "Devy Finn",
      chat : "Thanks you for quick response. I using iPhone 6s and the version of this is 10.2 . Please fix this issue I need this right now....Thanks",
      time : "7 Min ago",
      classSendBy : "sender"
    },
    {
      image : "assets/img/register-user-1.jpg",
      name : "Sam Brown",
      chat : "Please wait for some time. Our tecnical support team will contact you soon and fix the issue .Thanks for using our App.We will Assit You better",
      time : "6 Min ago",
      classSendBy : "receiver"
    }
  ]

  //traffic source content
  trafficSource : any[]= [
    {
      title : "Direct",
      icon : "arrow_drop_up",
      progress : 30,
      progress_color : "primary",
      icon_color : "text-alert"
    },
    {
      title : "Referral",
      icon : "arrow_drop_down",
      progress : 20,
      progress_color : "warn",
      icon_color : "text-danger"
    },
    {
      title : "Social",
      icon : "arrow_drop_up",
      progress : 60,
      progress_color : "accent",
      icon_color : "text-alert"
    },
    {
      title : "Internet",
      icon : "arrow_drop_up",
      progress : 70,
      progress_color : "primary",
      icon_color : "text-alert"
    },
    {
      title : "Ads",
      icon : "arrow_drop_up",
      progress : 85,
      progress_color : "warn",
      icon_color : "text-alert"
    }
  ]

  //notification content
  notificationContent : any = [
    {
      notification : "Site goes is down for 6 hours due to maintainance and bug fixing.Please Check",
      card_color : "warn-bg"
    },
    {
      notification : "New users from March is promoted as special benefit under promotional offer of 30%.",
      card_color : "success-bg"
    },
    {
      notification : "Bug detected from the development team at the cart module of Fashion store.",
      card_color : "primary-bg"
    }
  ]

  //social card content 
  socialCard : any = [
    {
      card_color : "primary-bg",
      icon : "fa-facebook",
      name : "FACEBOOK",
      follows : "41"
    },
    {
      card_color : "accent-bg",
      icon : "fa-twitter",
      name : "TWITTER",
      follows : "87"
    },
    {
      card_color : "warn-bg",
      icon : "fa-google-plus",
      name : "GOOGLE PLUS",
      follows : "17"
    }
  ]


  todos = [
      {newTodo:"Add widget to another site",completed:false},
      {newTodo:"Update the server no 2",completed:false},
      {newTodo:"Clean all junks now",completed:false},
      {newTodo:"Admin template optimize",completed:false},
      {newTodo:"Set record on piano tiles 2",completed:false},
      {newTodo:"Buy a fish for home",completed:false},
      {newTodo:"Wash the ear for holiday",completed:true},
      {newTodo:"Complete your task till Monday",completed:false},
      {newTodo:"Send mail to client",completed:false},
      {newTodo:"Submission of Project",completed:false},
      {newTodo:"Unit Testting for Errors",completed:false},
      {newTodo:"Resolving testing points",completed:false},
      {newTodo:"Analyis the whole project",completed:false},
  ];

   // Doughnut
  public doughnutChartLabels:string[] = ['Bounce', 'Open', 'Unsuscribe'];
  public doughnutChartData:number[] = [500, 250, 150];
  public doughnutChartType:string = 'doughnut';
  doughnutChartColors: any[] = [{
    backgroundColor: ['#32CBD8', '#E8C63B', '#f0f2f7']
  }];
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

  //Manage List
  list: Object[] = [{
    title: "5 Text editor that are free",
    photo: "assets/img/post1.jpg",
    desc: "Repellendus ipsum illum optio sequi at iste. Odit molestiae, voluptatem dignissimos. Necessitatibus dolore tempora error quia minus! Esse, quidem, impedit. Delectus itaque impedit excepturi.",
    author: "Admin",
    postdate: "3 Days Ago"
  },{
    title: "Know more about To Do List",
    photo: "assets/img/post2.jpg",
    desc: "Repellendus ipsum illum optio sequi at iste. Odit molestiae, voluptatem dignissimos. Necessitatibus dolore tempora error quia minus! Esse, quidem, impedit. Delectus itaque impedit excepturi.",
    author: "Help Desk",
    postdate: "1 Days Ago"
  },{
    title: "Latest Angular Admin Themes",
    photo: "assets/img/post3.jpg",
    desc: "Repellendus ipsum illum optio sequi at iste. Odit molestiae, voluptatem dignissimos. Necessitatibus dolore tempora error quia minus! Esse, quidem, impedit. Delectus itaque impedit excepturi.",
    author: "Kenny",
    postdate: "3 Hrous Ago"
  }];

  // Yearly sales
  public barChartLabels:string[] = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'];
  public barChartType:string = 'bar';
  public barChartLegend:boolean = false;

  public barChartData:any[] = [
    {data: [9,6,7,3,4,5,4,7,9,7], label: 'Series A'},
  ];
  
  public barStackChartOptions:any = {
    scaleShowVerticalLines: false,
    responsive: true,
    scales: {
      xAxes: [{
        stacked: false,
        barThickness: 18,
        display: true,
        gridLines: {
          color: 'rgba(0,0,0,0)',
          zeroLineColor: 'rgba(0,0,0,0)'
        },
        ticks: {
          beginAtZero: true,
          suggestedMax: 10
        }
      }],
      yAxes: [{
        stacked: true,
        barThickness: 18,
        gridLines: {
          color: 'rgba(255, 255, 255, 0.4)',
          zeroLineColor: 'rgba(255, 255, 255, 0.4)'
        }
      }]
    }
  };
  barChartColors: Array <any> = [{
    backgroundColor: '#fff',
    borderColor: 'rgba(0, 151, 167, 1)',
  }];

    // Bubble Chart
    bubbleChartData: Array <any> = [{
      data: [{
      x: 1,
      y: 2,
      r: 18,
    },{
      x: 1,
      y: 8,
      r: 12,
    }, {
      x: 3,
      y: 6,
      r: 12,
    }, {
      x: 5,
      y: 8,
      r: 18,
    }, {
      x: 7,
      y: 4,
      r: 12,
    }, {
      x: 9,
      y: 2,
      r: 15,
    }, {
      x: 9,
      y: 9,
      r: 12,
    }],
      label: 'Series A',
      borderWidth: 1
    }];
    bubbleChartType = 'bubble';
    bubbleChartColors: Array <any> = [{
      backgroundColor: 'rgba(67, 210, 158, 1)',
      borderColor: 'rgba(67, 210, 158, 1)',
      pointBackgroundColor: 'rgba(67, 210, 158, 1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(67, 210, 158, 0.8)'
  }];
  public bubbleChartOptions:any = {
    responsive: true,
    scales: {
      xAxes: [{
        gridLines: {
          color: 'rgba(0,0,0,0)',
          zeroLineColor: 'rgba(0,0,0,0)',
        },
        ticks: {
          beginAtZero: true,
          suggestedMax: 10
        }
      }],
      yAxes: [{
        gridLines: {
          color: 'rgba(0,0,0,0.09)',
          zeroLineColor: 'rgba(0,0,0,0.09)'
        },
        ticks: {
          beginAtZero: true,
          suggestedMax: 10
        }
      }]
    }
  };

  public customStyle = [{"featureType":"water","elementType":"geometry","stylers":[{"color":"#e9e9e9"},{"lightness":17}]},{"featureType":"landscape","elementType":"geometry","stylers":[{"color":"#f5f5f5"},{"lightness":20}]},{"featureType":"road.highway","elementType":"geometry.fill","stylers":[{"color":"#ffffff"},{"lightness":17}]},{"featureType":"road.highway","elementType":"geometry.stroke","stylers":[{"color":"#ffffff"},{"lightness":29},{"weight":0.2}]},{"featureType":"road.arterial","elementType":"geometry","stylers":[{"color":"#ffffff"},{"lightness":18}]},{"featureType":"road.local","elementType":"geometry","stylers":[{"color":"#ffffff"},{"lightness":16}]},{"featureType":"poi","elementType":"geometry","stylers":[{"color":"#f5f5f5"},{"lightness":21}]},{"featureType":"poi.park","elementType":"geometry","stylers":[{"color":"#dedede"},{"lightness":21}]},{"elementType":"labels.text.stroke","stylers":[{"visibility":"on"},{"color":"#ffffff"},{"lightness":16}]},{"elementType":"labels.text.fill","stylers":[{"saturation":36},{"color":"#333333"},{"lightness":40}]},{"elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"geometry","stylers":[{"color":"#f2f2f2"},{"lightness":19}]},{"featureType":"administrative","elementType":"geometry.fill","stylers":[{"color":"#fefefe"},{"lightness":20}]},{"featureType":"administrative","elementType":"geometry.stroke","stylers":[{"color":"#fefefe"},{"lightness":17},{"weight":1.2}]}]

  // lineChart
  public lineChartData:Array<any> = [
    {data: [90, 150, 80, 300, 90, 290, 350,200,80,100,220,230,310,230,150,180,120,150], label: 'Series A'},
    {data: [110, 90, 150, 130, 290, 210, 200,80,80,110,320,310,50,170,210,310,150,80,450], label: 'Series B'},
  ];
  public lineChartLabels:Array<any> = ['1', '2', '3', '4', '5', '6', '7','8','9','10','11','12','13','14','15','16','17','18'];
  public lineChartOptions:any = {
    responsive: true,
    scales: {
      xAxes: [{
        ticks: {
          beginAtZero: true,
          suggestedMax: 450
        }
      }]
    }
  };
  lineChartColors: Array <any> = [{
    backgroundColor: 'rgba(235, 78, 54, 0.2)',
    borderColor: 'rgba(235, 78, 54, 1)',
    pointBackgroundColor: 'rgba(235, 78, 54, 1)',
    pointBorderColor: '#fff',
    pointHoverBackgroundColor: '#fff',
    pointHoverBorderColor: 'rgba(235, 78, 54, 0.8)'
  }, {
    backgroundColor: 'rgba(0, 151, 167, 0.2)',
    borderColor: 'rgba(0, 151, 167, 1)',
    pointBackgroundColor: 'rgba(0, 151, 167, 1)',
    pointBorderColor: '#fff',
    pointHoverBackgroundColor: '#fff',
    pointHoverBorderColor: 'rgba(0, 151, 167, 0.8)'
  }];
  public lineChartLegend:boolean = false;
  public lineChartType:string = 'line';

	// Team
  team: Object[] = [{
    name: 'Isabela Phelaps',
    photo: 'assets/img/user-1.jpg',
    post: 'Sr.Manager',
  }, {
    name: 'Trevor Hansen',
    photo: 'assets/img/user-2.jpg',
    post: 'Manager',
  }, {
    name: 'Sandra Adams',
    photo: 'assets/img/user-3.jpg',
    post: 'Engineer',
  },{
    name: 'Sandy Smith',
    photo: 'assets/img/user-4.jpg',
    post: 'Engineer',
  },{
    name: 'Rosy Wonn',
    photo: 'assets/img/user-5.jpg',
    post: 'Jr.Engineer',
  },{
    name: 'Alex Roddy',
    photo: 'assets/img/user-6.jpg',
    post: 'Jr.Engineer',
  }];

  constructor( private pageTitleService: PageTitleService,
               private translate : TranslateService,
               public service : CoreService) {

    this.fetch((data) => { this.rows = data; });
    this.newTodo = '';
  }
  
  ngOnInit() {
    this.pageTitleService.setTitle("Home");
  }

	// project table
	fetch(cb) {
  	const req = new XMLHttpRequest();
  	req.open('GET', `assets/data/projects.json`);
  	req.onload = () => {
    	cb(JSON.parse(req.response));
  	};
  	req.send();
	}

  //addTodo method is used to add a new item into ToDo List.
  addTodo(event) {
    this.todoObj = {
      newTodo: this.newTodo,
      completed: false
    }
    this.todos.push(this.todoObj);
    this.newTodo = '';
    event.preventDefault();
  }

  //onDeleteTodoList method is used to delete the to do list.
  onDeleteTodoList(index){
    this.service.deleteDialog("Are you sure you want to delete this list permanently?").
      subscribe(res => {this.popUpDeleteResponse = res},
                err => console.log(err),
                ()  => this.getPopUpDeleteResponse(this.popUpDeleteResponse, index))
  }

  //getPopUpDeleteResponse method is used when get the delete response 'yes' then list delete from the to do list.
  getPopUpDeleteResponse(response, index){
    if(response == 'yes'){
      this.todos.splice(index,1);
    }
  }
}
