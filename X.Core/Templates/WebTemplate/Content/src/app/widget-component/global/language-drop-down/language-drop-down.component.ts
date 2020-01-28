import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core'
import { TranslateExtService } from 'app/shared/services/translate-ext.service';

@Component({
  selector: 'ms-language-drop-down',
  templateUrl: './language-drop-down.component.html',
  styleUrls: ['./language-drop-down.component.scss']
})
export class LanguageDropDownComponent implements OnInit {

   currentLang = 'en';
   selectImage = 'assets/img/en.png';
   languages=[];

	constructor(public translate : TranslateService, public translateExt: TranslateExtService) { }

	ngOnInit() {
      this.languages = this.translateExt.getLanguages();
      this.setLang(this.translateExt.getCurrentLanguage());
	}

   //setLang method is used to set the language into template.
   setLang(lang) {
      this.selectImage = this.translateExt.getLanguages().find(x => x.value == lang).img;
      for(let data of this.translateExt.getLanguages()) {
         if(data.value == lang) {
            this.selectImage = data.img;
            this.translateExt.setLocale(lang);
            break;
         }
      }
      
   }
}
