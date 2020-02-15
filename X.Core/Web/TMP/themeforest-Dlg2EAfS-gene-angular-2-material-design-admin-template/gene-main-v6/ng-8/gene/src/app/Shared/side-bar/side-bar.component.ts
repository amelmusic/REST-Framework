import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router, NavigationEnd,ActivatedRoute } from '@angular/router';
import { CoreService } from '../../service/core/core.service';
import { MenuItems } from '../../core/menu/menu-items/menu-items';

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
                public coreService: CoreService,
                public menuItems: MenuItems) { }

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

	/**
     * addMenuItem is used to add a new menu into menu list.
     */
    addMenuItem(): void {
      this.menuItems.add({
         state: 'pages',
         name: 'GENE MENU',
         type: 'sub',
         icon: 'trending_flat',
         children: [
            {state: 'blank', name: 'SUB MENU1'},
            {state: 'blank', name: 'SUB MENU2'}
         ]
      });
   }

}
