import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { CoreService } from '../../service/core/core.service';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core'

@Component({
  selector: 'ms-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {
   
   tableTabData : any;
   //Invoices content
	invoicelist : any [] = [
      {
         id:"#inv001",
      	firstName :"Steven",
         lastName :"Gonz",
         accountTypeColor:"primary",
         accountType:"Paid",
         dateCreated:"13 Aug 2017",
         dueDate:"4 Jan 2018",
         amount:"$1000"
      },
      {
         id:"#inv002",
         firstName :"Joseph",
         lastName :"Good",
         accountType:"partially paid",
         accountTypeColor:"accent",
         dateCreated:"22 Aug 2017",
         dueDate:"28 Feb 2019",
         amount:"$2500"
     	},
      {
         id:"#inv003",
         firstName :"Mario",
         lastName :"Harmon",
			accountType:"paid",
         accountTypeColor:"primary",
         dateCreated:"13 Aug 2017",
         dueDate:"10 Mar 2018",
         amount:"$500"
      },
      {
         id:"#inv004",
         firstName :"Aleta",
         lastName :"Good",
			accountType:"unpaid",
         accountTypeColor:"warn",
         dateCreated:"22 Aug 2017",
         dueDate:"23 Aug 2019",
         amount:"$700"
      },
      {
         id:"#inv005", 
         firstName :"Floren",
         lastName :"Smith",
         accountType:"partially paid",
         accountTypeColor:"accent",
         dateCreated:"13 Aug 2018",
         dueDate:"25 June 2018",
         amount:"$1090"
      },
      {
         id:"#inv006",
         firstName :"Helen",
         lastName :"Moron",
         accountType:"unpaid",
         accountTypeColor:"warn",
         dateCreated:"22 Aug 2017",
         dueDate:"29 Nov 2018",
         amount:"$1900"
      }
   ];

   //Payments content
   paymentlist : any [] = [
      {
         payid:"#pay001",
      	firstName :"Leonard",
         lastName :"Gonz",
         paymentType:"Paypal",
         paymentTypeColor:"primary",
         paidDate: "19 Aug 2017",
         amount:"$2000"
      },
      {
         payid:"#pay002",
         firstName :"Agnes",
         lastName :"Good",
         paymentType:"Paytm",
         paymentTypeColor:"accent",
         paidDate: "22 Mar 2017",
         amount:"$500"
     	},
      {
         payid:"#pay003",
         firstName :"Bonnie",
         lastName :"Harmon",
         paymentType:"Debit Card",
         paymentTypeColor:"primary",
         paidDate: "30 Sep 2017",
         amount:"$1500"
      },
      {
         payid:"#pay004",
         firstName :"Virgil",
         lastName :"Good",
         paymentType:"Credit Card",
         paymentTypeColor:"accent",
         paidDate: "20 Aug 2017",
         amount:"$1700"
      },
      {
         payid:"#pay005", 
         firstName :"Kevin",
         lastName :"Smith",
         paymentType:"paypal",
         paymentTypeColor:"primary",
         paidDate: "13 Aug 2018",
         amount:"$1290"
      },
      {
         payid:"#pay006",
         firstName :"Alice",
         lastName :"Moron",
         paymentType:"Phone pe",
         paymentTypeColor:"warn",
         paidDate: "22 Aug 2017",
         amount:"$1500"
      }
   ];

   //Tax Rates content
   taxrates : any [] = [
      {
         date:"4 Jan 2018",
         account:"The Bank of America",
         TypeColor:"primary",
         type:"Expense",
         amount:"$1000.00",
         credit:"$300.00",
         balance:"$200.00"
      },
      {
         date:"28 Feb 2019",
         account:"Barclays Bank",
         TypeColor:"accent",
         type:"Income",
         amount:"$2500.00",
         credit:"$200.00",
         balance:"$150.00"
     	},
      {
         date:"10 Mar 2018",
			account:"Bank of Scotland",
         TypeColor:"primary",
         type:"Saving",
         amount:"$500.00",
         credit:"$100.00",
         balance:"$50.00"
         
      },
      {
         date:"23 Aug 2019",
			account:"Deutsche Bank",
         TypeColor:"warn",
         type:"Income",
         amount:"$700.00",
         credit:"$300.00",
         balance:"$200.00"
      
      },
      {
         date:"25 June 2018",
         account:"HSBC Bank",
         TypeColor:"accent",
         type:"Saving",
         amount:"$1090.00",
         credit:"$800.00",
         balance:"$600.00"
         
      },
      {
         date:"29 Nov 2018",
         account:"HSBC Bank",
         TypeColor:"warn",
         type:"Expense",
         amount:"$1900.00",
         credit:"$600.00",
         balance:"$400.00"
        
      }
   ];

   //Add Tickets content
   addtickets : any [] = [
      {
         srno:"01",
      	ticketCode:"TRC 45651",
         subject:"Fly Bimen",
         date: "19 Aug 2017",
         department:"First Class",
         status:"Booked",
         statusColor:"primary"
      },
      {
         srno:"02",
         ticketCode:"TRC 45652",
         subject:"Fly Emeters",
         date: "22 Mar 2017",
         department:"Second Class",
         status:"cancel",
         statusColor:"warn"
     	},
      {
         srno:"03",
         ticketCode:"TRC 45653",
         subject:"Air India",
         date: "30 Sep 2017",
         department:"First Class",
         status:"Booked",
         statusColor:"primary"
      },
      {
         srno:"04",
         ticketCode:"TRC 45654",
         subject:"Air India",
         date: "20 Aug 2017",
         department:"First Class",
         status:"Booked",
         statusColor:"primary"
      },
      {
         srno:"05",
         ticketCode:"TRC 45655",
         subject:"Air India",
         date: "13 Aug 2018",
         department:"Second Class",
         status:"cancel",
         statusColor:"warn"
      },
      {
         srno:"06",
         ticketCode:"TRC 45656",
         subject:"Air India",
         date: "22 Aug 2017",
         department:"First Class",
         status:"booked",
         statusColor:"primary"
      }
   ];


   displayedInvoiceColumns : string [] = ['id','clientName','accountType','dateCreated','dueDate', 'amount'];
   
   displayedPaymentColumns : string [] = ['payid','clientName', 'paymentType','paidDate', 'amount'];

   displayedTransactionColumns : string [] = ['transid','date','account', 'type', 'amount','debit','credit', 'balance'];

   displayedTransferColumns : string [] = ['transid','date','account', 'type', 'amount', 'balance','status'];

   displayedExpenseColumns : string [] = ['itmNo','date', 'type','description','amount','status'];

   displayedTaxColumns : string [] = ['date','account', 'type', 'amount', 'credit', 'balance'];

   displayedAddTicketsColumns : string [] = ['srno','ticketCode', 'subject','date','department','status',];

   constructor ( private pageTitleService : PageTitleService,
                 private translate : TranslateService,
                 private service : CoreService ) { }

   ngOnInit () { 
      this.pageTitleService.setTitle("Reports");

      this.service.getTableTabContent().
         subscribe( res => { this.tableTabData = res },
                    err => console.log(err),
                    ()  => this.tableTabData
                  );
   }

}
