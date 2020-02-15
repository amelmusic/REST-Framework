import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'ms-blank',
    templateUrl: './blank.component.html',
    styleUrls: ['./blank.component.scss'],
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class BlankComponent implements OnInit {

  constructor(private pageTitleService: PageTitleService, private translate : TranslateService) { }

  ngOnInit() {
  	this.pageTitleService.setTitle("Blank");
  }

}
