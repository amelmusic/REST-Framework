import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { CoreService } from '../../service/core/core.service';

@Component({
  selector: 'ms-user-grid-list',
  templateUrl: './user-grid-list.component.html',
  styleUrls: ['./user-grid-list.component.scss']
})
export class UserGridListComponent implements OnInit {
 
	userGridList : any;

	constructor(private pageTItleService : PageTitleService,
					public coreService : CoreService) { }

	ngOnInit() {
		this.pageTItleService.setTitle("User Grid List");
		this.coreService.getUserManagementGridList().
			subscribe( res => { this.userGridList = res },
						  err => console.log(err),
						  ()  => this.userGridList
						);
	}

}
