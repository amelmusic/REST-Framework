import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router, NavigationEnd,ActivatedRoute } from '@angular/router';
import { MenuItems } from '../../core/menu/menu-items/menu-items';
import { CoreService } from '../services/core.service';
import { OAuthService } from 'angular-oauth2-oidc';

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
                public menuItems: MenuItems, private oauthService: OAuthService) { }

	ngOnInit() {
	}

   public get name() {
      const claims = <any>this.oauthService.getIdentityClaims();
      if (!claims) {
        return null;
      }
      return claims.given_name + ' ' + claims.family_name;
   }

	//render to the crm page
	onClick(){

	}
}
