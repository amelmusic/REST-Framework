import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot,Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

	constructor(private router: Router, private oauthService: OAuthService) { }
	
	async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const claims = this.oauthService.getIdentityClaims();
    if (!claims) {
      const result = await this.oauthService.loadDiscoveryDocumentAndLogin();
      return result;
    } else {
      return claims != null;
    }
  }
}