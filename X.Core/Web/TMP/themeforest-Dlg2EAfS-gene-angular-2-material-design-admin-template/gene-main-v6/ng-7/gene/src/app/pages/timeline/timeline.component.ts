import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { CoreService } from '../../service/core/core.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'ms-timeline',
    templateUrl: './timeline.component.html',
    styleUrls: ['./timeline.component.scss'],
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class TimelineComponent implements OnInit {

   constructor(private pageTitleService: PageTitleService,
               private coreService : CoreService, private translate : TranslateService) { }

   ngOnInit() {
      this.pageTitleService.setTitle("Timeline");
   }

   //onVideoPlayer method is used to open a video player dialog.
   onVideoPlayer(){
      this.coreService.videoPlayerDialog("https://www.youtube.com/watch?v=rbTVvpHF4cU");
   }

}
