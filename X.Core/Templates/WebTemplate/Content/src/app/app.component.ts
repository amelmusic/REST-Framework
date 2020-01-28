import {Component, Optional, ViewEncapsulation} from '@angular/core';
import { TranslateService} from '@ngx-translate/core';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { environment } from 'environments/environment';
import { TranslateExtService } from './shared/services/translate-ext.service';
import { LoadingBarService } from '@ngx-loading-bar/core';
import { map, take, delay, withLatestFrom, finalize, tap } from 'rxjs/operators';

@Component({
  	selector: 'xcore-app',
   template:`<router-outlet></router-outlet>`,
   encapsulation: ViewEncapsulation.None
})

export class XCoreAppComponent {

   delayedProgress$ = this.loader.progress$.pipe(
      delay(2000),
      withLatestFrom(this.loader.progress$),
      map(v => v[1]),
    );

   constructor(translate: TranslateService, translateExtService: TranslateExtService, private oauthService: OAuthService, public loader: LoadingBarService) {
      const langs = translateExtService.getLanguages().map(x => x.value);
      translate.addLangs(langs);
      if (localStorage.getItem('core.language')) {
         translateExtService.setLocale(localStorage.getItem('core.language'));
       } else {
         translateExtService.setLocale('en');
       }

      this.auth();
   }

   auth() {
      this.oauthService.configure(environment.auth);
      this.oauthService.tokenValidationHandler = new JwksValidationHandler();
      this.oauthService.loadDiscoveryDocumentAndTryLogin({
         onTokenReceived: context => {
            
         }
      });
   }
}
