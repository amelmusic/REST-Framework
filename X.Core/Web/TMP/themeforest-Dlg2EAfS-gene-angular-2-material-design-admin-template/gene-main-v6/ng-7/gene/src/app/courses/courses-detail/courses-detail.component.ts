import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation } from "../../core/route-animation/route.animation";
import { CoreService } from '../../service/core/core.service';

@Component({
   selector: 'ms-courses-detail',
   templateUrl: './courses-detail.component.html',
   styleUrls: ['./courses-detail.component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ],  
})

export class CoursesDetailComponent implements OnInit {

   jsonResponse : any = [];

   constructor(private pageTitleService : PageTitleService,
               private coreService : CoreService,
               private translate : TranslateService) { }

   ngOnInit() { 
      this.pageTitleService.setTitle("Course Detail");
      this.getCourses();
   }

   /** 
     * getCourses method is used to get the courses list from JSON file.
     */
   getCourses() {
      this.coreService.getCourses().
            subscribe( res => { this.jsonResponse = res },
                       err => console.log(err),
                       ()  => this.jsonResponse
                     );
   }

}
