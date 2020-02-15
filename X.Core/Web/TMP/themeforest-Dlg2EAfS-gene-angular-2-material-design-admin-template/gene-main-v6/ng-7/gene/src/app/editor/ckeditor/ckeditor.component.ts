import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import './ckeditor.loader';
import 'ckeditor';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-ckeditor',
   templateUrl: './ckeditor.html',
   styleUrls: ['./ckeditor.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class Ckeditor implements OnInit {
   public ckeditorContent:string = '<p>Hello CKEditor</p>';
   public config = {
      uiColor: '#F0F3F4',
      height: '600',
   };

   constructor(private pageTitleService: PageTitleService,
               private translate : TranslateService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Ckeditor");
   }  
}
