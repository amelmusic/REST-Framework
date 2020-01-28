import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DateAdapter } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class TranslateExtService {

  constructor(
      private translateService: TranslateService,
      private dateAdapter: DateAdapter<any>) {

  }
  async translate(key) {
      let observable = null;
      const promise = new Promise(resolve => {
          observable = this.translateService.get(key)
              .subscribe(
              (res: string) => {
                  resolve(res);
              });
      });
      const result: any = await promise;
      observable.unsubscribe();
      return result;
  }

  setLocale(locale: string) {
      this.translateService.use(locale);
      if (locale === 'bs') {
          this.dateAdapter.setLocale('sr-Latn');
      } else {
          this.dateAdapter.setLocale(locale);
      }
      localStorage.setItem('core.language', locale);
  }

  getLangs(): string[] {
      return this.translateService.getLangs();
  }

  getCurrentLanguage(): string {
      return this.translateService.currentLang;
  }

  getLanguages(): any[] {
      return [{ name: 'English', value: 'en', img:"assets/img/en.png" },
      { name: 'Bosanski', value: 'bs', img:"assets/img/ba.png" },
      { name: 'German', value: 'de', img:"assets/img/german.png" }]
  }

}
