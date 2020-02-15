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

  constructor(private pageTitleService: PageTitleService, private translate : TranslateService) {
  }

  ngOnInit() {
    this.pageTitleService.setTitle("Gallery");
  }

}



