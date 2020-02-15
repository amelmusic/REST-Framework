import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   'selector': 'ms-media-gallery',
   templateUrl:'./media-component.html',
   styleUrls: ['./media-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class MediaComponent implements OnInit {

   mediaContent : any = [
      {
         image : "assets/img/Blue-Vintag.jpg",
         title : "Vintage Cars"
      },
      {
         image : "assets/img/Cross-Rode.jpg",
         title : "Cross Rode"
      },
      {
         image : "assets/img/Flower.jpg",
         title : "Yellow Flower"
      },
      {
         image : "assets/img/Code-Editor.jpg",
         title : "Code Editor"
      },
      {
         image : "assets/img/Vintag-Cars.jpg",
         title : "Wonder Cars"
      },
       {
         image : "assets/img/Blue-Heaven.jpg",
         title : "Blur Blue"
      },
      {
         image : "assets/img/Home.jpg",
         title : "Home"
      },
      {
         image : "assets/img/green.jpg",
         title : "Nature"
      },
      {
         image : "assets/img/white.jpg",
         title : "Hill Station"
      }
   ]

   constructor(private pageTitleService: PageTitleService, private translate : TranslateService) {
   }

   ngOnInit() {
      this.pageTitleService.setTitle("Gallery");
   }

}



