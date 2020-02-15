import {forkJoin as observableForkJoin} from 'rxjs';
import { map } from 'rxjs/operators';
import {HttpClient} from '@angular/common/http';
import {TranslateLoader} from '@ngx-translate/core';

export class MultiTranslateHttpLoader implements TranslateLoader {

  constructor(private http: HttpClient,
              public resources: { prefix: string, suffix: string }[] = [{
                prefix: '/assets/i18n/',
                suffix: '.json'
              }]) {}

  /**
   * Gets the translations from the server
   * @param lang
   * @returns {any}
   */
  public getTranslation(lang: string): any {

    return observableForkJoin(this.resources.map(config => {
      return this.http.get(`${config.prefix}${lang}${config.suffix}`);
    }))
        .pipe(map(response => {
      return response.reduce((a: any, b: any) => {
        a._body = a; // JSON.parse(a._body);
        b._body = b; // JSON.parse(b._body);
        const obj: any = Object.assign(a._body, b._body);
        return obj;
      });
    }));
  }
}
