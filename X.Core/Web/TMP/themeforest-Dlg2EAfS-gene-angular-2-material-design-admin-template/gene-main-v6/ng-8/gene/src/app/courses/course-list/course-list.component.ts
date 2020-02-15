import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CoreService } from '../../service/core/core.service';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation } from "../../core/route-animation/route.animation";

@Component({
   selector: 'ms-course-list',
   templateUrl: './course-list.component.html',
   styleUrls: ['./course-list.component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]    
})

export class CourseListComponent implements OnInit {

   jsonResponse : any = [];

   constructor( private pageTitleService : PageTitleService, 
                private translate : TranslateService,
                private coreService : CoreService) { }

   ngOnInit() {
      this.pageTitleService.setTitle("Course List");
      this.getCourses();
   }

   /**
     * getCourses method is used to get the courses list.
     */
   getCourses(){
      this.coreService.getCourses().
            subscribe( res => { this.jsonResponse = res },
                       err => console.log(err),
                       ()  => this.jsonResponse
                     );
   };

   /**
     * getCoursesByPopularity method is used to get the popularity of courses.
     */
   getCoursesByPopularity(type) {
      let course = []; 
      if(this.jsonResponse.courses && this.jsonResponse.courses.length>0) 
         {
            for (let list of this.jsonResponse.courses){
               if(list.popular == type){
                  course.push(list);
               }
            }
            return course;
         }
   }
}
