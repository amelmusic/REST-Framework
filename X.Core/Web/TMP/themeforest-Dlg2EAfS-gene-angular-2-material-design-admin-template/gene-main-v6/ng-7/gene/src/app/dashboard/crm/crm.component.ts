import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { CoreService } from '../../service/core/core.service';

@Component({
  selector: 'ms-crm',
  templateUrl: './crm.component.html',
  styleUrls: ['./crm.component.scss']
})

export class CrmComponent implements OnInit {
	
   tableTabData : any;
   statsCards   : any;

   //upcoming events content
	UpcomingEvents : any [] = [
		{
			title : "Marketing Seminar",
			date : "28th April",
			status : "Email",
			location : "Mumbai"
		},
		{
			title : "Strategy Planning",
			date : "22th May",
			status : "Phone",
			location : "Delhi"
		},
		{
			title : "Hiring Personals",
			date : "29th May",
			status : "Skype",
			location : "Delhi"
		},
		{
			title : "Training",
			date : "30th May",
			status : "Email",
			location : "Delhi"
		}
	]

   //project status content
	projectStatus : any [] = [
		{
			title : "Project 1",
			duration : "Completed",
			color : "success-bg",
			value : 50
		},
		{
			title : "Project 2",
			duration : "Pending",
			color : "warn-bg",
			value : 60
		},
		{
			title : "Project 3",
			duration : "Ongoing",
			color : "primary-bg",
			value : 70
		},
		{
			title : "Project 4",
			duration : "Completed",
			color : "success-bg",
			value : 50
		}
	]

   //sale chart data 
   saleChartData = [{
      "title": "Product 1",
      "value": 351.9
      },{
      "title": "Product 2",
      "value": 165.8
      }, {
      "title": "Product 3",
      "value": 139.9
      }, {
      "title": "Product 4",
      "value": 128.3
   }];

   displayedTransactionColumns : string [] = ['transid','date','account', 'type', 'amount','debit','credit', 'balance'];

   displayedTransferColumns : string [] = ['transid','date','account', 'type', 'amount', 'balance','status'];

   displayedExpenseColumns : string [] = ['itmNo','date', 'type','description','amount','status'];
     
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

	constructor(private pageTitleService: PageTitleService,
               private coreService : CoreService) { }

	ngOnInit() {
	   this.pageTitleService.setTitle("CRM");
      this.coreService.getTableTabContent().
         subscribe( res => { this.tableTabData = res },
                    err => console.log(err),
                    ()  => this.tableTabData
          );

      this.coreService.getCrmStatsCardContent().
         subscribe( res => { this.statsCards = res },
                    err => console.log(err),
                    ()  => this.statsCards
          );
	}
}
