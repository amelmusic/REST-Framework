import { Injectable } from '@angular/core';
import { HttpContextService } from './http-context.service';
import { environment } from 'environments/environment';
import { OAuthService } from 'angular-oauth2-oidc';

export interface ChildrenItems {
	state: string;
	name: string;
	type?: string;
  }
  
  export interface Menu {
	state: string;
	name: string;
	type: string;
	icon: string;
	children?: ChildrenItems[];
  }

@Injectable({
	providedIn: 'root'
})

export class CoreService {

	collapseSidebar: boolean = false;
	collapseSidebarStatus: boolean;
	sidenavMode: string = "side";
	sidenavOpen: boolean = true;
	horizontalSideNavMode: string = "over";
	horizontalSideNavOpen: boolean = false;

	constructor(private httpContextService: HttpContextService, private oauthService: OAuthService) {
	}

	async permissionCheck(request: any) {
		//TODO: POPULATE CORRECT URL HERE FOR PERMISSION CHECKING!
		let absoluteBasePath = environment.pathList.permission; //for now simply take first one
		
		if (!absoluteBasePath) {
			throw new Error("Permission base path must be populated!");
		}
		if (!request) {
			throw new Error("Request must be populated!");
		}
		if (!request.permission) {
			return true;
		}
		if (!request.operationType) {
			throw new Error("operationType must be populated!");
		}
		let url = absoluteBasePath + "/" + "PermissionCheck/Check";
		return await this.httpContextService.get(url, request);
	}

	allowedItemsFiltered = null;
	async getMenu(menuTitems): Promise<Menu[]> {
		let items = menuTitems;
		if (this.allowedItemsFiltered === null) {
		  let allowedItems = [];
		  for(var item of items) {
			var allowed = await this.permissionCheck({permission: item.permission, operationType: 'View'});
			if (allowed) {
			  allowedItems.push(item);
			}
		  }
		  this.allowedItemsFiltered = allowedItems;
		}
	
		return this.allowedItemsFiltered;
	  }

	  async logIn() {
		// this.oauthService.log
		await this.oauthService.loadDiscoveryDocumentAndLogin();
	  }

	  logOut() {
		this.oauthService.logOut();
	  }

	  isLoggedIn() {
		const claims = this.oauthService.getIdentityClaims();
		return claims != null;
	  }

	  claims() {
		const claims = this.oauthService.getIdentityClaims();
		return claims;
	  }
}