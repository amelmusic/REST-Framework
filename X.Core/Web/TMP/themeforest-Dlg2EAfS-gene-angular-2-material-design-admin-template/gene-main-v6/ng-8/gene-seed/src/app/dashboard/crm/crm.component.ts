import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';

@Component({
  selector: 'ms-crm',
  templateUrl: './crm.component.html',
  styleUrls: ['./crm.component.scss']
})

export class CrmComponent implements OnInit {
	
   constructor(private pageTitleService: PageTitleService) { }

   ngOnInit() {
      this.pageTitleService.setTitle("CRM");
   }

}
