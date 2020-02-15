import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router, NavigationEnd,ActivatedRoute } from '@angular/router';
import { CoreService } from '../../service/core/core.service';

@Component({
	selector: 'ms-side-bar',
	templateUrl: './side-bar.component.html',
	styleUrls: ['./side-bar.component.scss']
})

export class SideBarComponent implements OnInit {

	@Input() menuList : any;
  @Input() verticalMenuStatus : boolean;

	constructor( public translate: TranslateService, 
                private router: Router,
                public coreService: CoreService) { }

	ngOnInit() {
	}

	//render to the crm page
	onClick(){
		var first = location.pathname.split('/')[1];
      if(first == 'horizontal'){
         this.router.navigate(['/horizontal/dashboard/crm']);
      }else {
         this.router.navigate(['/dashboard/crm']);
      }
	}

}
