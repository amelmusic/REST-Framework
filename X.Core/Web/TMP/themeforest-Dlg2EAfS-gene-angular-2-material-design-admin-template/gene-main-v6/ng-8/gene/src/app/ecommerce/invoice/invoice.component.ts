import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PageTitleService } from '../../core/page-title/page-title.service';

@Component({
  selector: 'ms-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss']
})
export class InvoiceComponent implements OnInit {

	/*
      ---------- Invoice Component  ----------
   */

   // invoicesData arary of object data is used in Invoices Component.      
   invoicesData : any [] = [
      {
         qty:'01',
         description:'iPhone5 32GB White & Silver(GSM) Unlocked',
         unit_price:749,
         total:749
      },
      {
         qty:'02',
         description:'iPhone5 32GB White & Silver(GSM) Unlocked',
         unit_price:749,
         total:749
      },
      {
         qty:'03',
         description:'iPhone5 32GB White & Silver(GSM) Unlocked',
         unit_price:749,
         total:749
      },
      {
         qty:'04',
         description:'iPhone5 32GB White & Silver(GSM) Unlocked',
         unit_price:749,
         total:749
      },
      {
         unit_price:'Subtotal',
         total:1607
      },
      {
         unit_price:'Shipping',
         total:0
      },
      {
         unit_price:'Total',
         total:1607
      }

   ]

   // displayedColumns contains list of the columns that used in Invoice Table. 
   displayedColumns : string[] = ['qty', 'description', 'unit_price', 'total'];

	constructor(private translate : TranslateService,private pageTitle:PageTitleService) { }

	ngOnInit() {
      this.pageTitle.setTitle("Invoice");
	}

}
