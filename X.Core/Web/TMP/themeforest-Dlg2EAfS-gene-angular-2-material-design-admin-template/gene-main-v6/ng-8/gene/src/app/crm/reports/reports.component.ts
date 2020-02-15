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
   invoicelist  : any;
   paymentlist  : any;
   taxrates     : any;
   addtickets   : any;

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

      this.service.getInvoiceListContent().
         subscribe( res => { this.invoicelist = res },
                    err => console.log(err),
                    ()  => this.invoicelist
                  );

      this.service.getPaymentList().
         subscribe( res => { this.paymentlist = res },
                    err => console.log(err),
                    ()  => this.paymentlist
                  );

      this.service.getTaxRateList().
         subscribe( res => { this.taxrates = res },
                    err => console.log(err),
                    ()  => this.taxrates
                  );

      this.service.getTicketList().
         subscribe( res => { this.addtickets = res },
                    err => console.log(err),
                    ()  => this.addtickets
                  );
   }

}
