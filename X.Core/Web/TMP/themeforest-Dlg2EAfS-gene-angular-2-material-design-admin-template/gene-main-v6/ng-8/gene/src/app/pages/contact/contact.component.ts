import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { PageTitleService } from '../../core/page-title/page-title.service';
import { CoreService } from '../../service/core/core.service';

@Component({
  selector: 'ms-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

	contactList : any;
	
	constructor(private pageTitleService: PageTitleService, 
				private translate : TranslateService,
				private service : CoreService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Contact");

		this.service.getContactListContent().
			subscribe( res => { this.contactList = res },
					   err => console.log(err),
					   () => { this.contactList }
					);
	}

}
