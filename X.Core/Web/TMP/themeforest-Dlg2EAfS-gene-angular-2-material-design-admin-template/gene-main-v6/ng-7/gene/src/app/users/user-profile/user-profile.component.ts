import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";

@Component({
    selector: 'ms-userprofile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.scss'],
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class UserProfileComponent implements OnInit {

  constructor(private pageTitleService: PageTitleService) { }

  ngOnInit() {
  	this.pageTitleService.setTitle("User Profile");
  }

}
