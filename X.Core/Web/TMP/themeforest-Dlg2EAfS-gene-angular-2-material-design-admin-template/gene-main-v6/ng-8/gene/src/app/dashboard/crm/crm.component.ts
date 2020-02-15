import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { CoreService } from '../../service/core/core.service';

@Component({
  selector: 'ms-crm',
  templateUrl: './crm.component.html',
  styleUrls: ['./crm.component.scss']
})

export class CrmComponent implements OnInit {
	
   tableTabData    : any;
   statsCards      : any;
   liveChatSupport : any;
   
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
   
   /*
      ----------Project Status Chart ----------
   */
  
   // bar chart label
   public projectStatusLabel :string[] = ['Project 1', 'Project 2', 'Project 3', 'Project 4'];
   
   //bar chart data
   public projectStatusData : any[] = [
      {data: [400 ,700, 1400, 900]}
   ];

   //bar chart color
   public projectStatusColors: Array <any> = [
      {
         backgroundColor: '#1565c0',
         hoverBackgroundColor: '#6794dc'
      }
   ]

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

      this.coreService.getLiveChatContent().
         subscribe(res => {this.liveChatSupport = res},
                   err => console.log(err),
                   ()  => this.liveChatSupport
         );
	}
}
