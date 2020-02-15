import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot,Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { CoreService } from './core.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

	constructor(private router: Router, private oauthService: OAuthService, private coreService: CoreService) { }
	
	async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const claims = this.coreService.isLoggedIn();
    if (!claims) {
      const result = await this.coreService.logIn();//this.oauthService.loadDiscoveryDocumentAndLogin();
      return false;
    } else {
      return claims != null;
    }
  }
}