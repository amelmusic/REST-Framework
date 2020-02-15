import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation} from "../../core/route-animation/route.animation";
import { CoreService } from '../../service/core/core.service';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-courses',
   templateUrl: './courses.component.html',
   styleUrls: ['./courses.component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ],
    
   
})
export class CoursesComponent implements OnInit {

   jsonResponse : any = [];

   constructor(private pageTitleService : PageTitleService,
               private coreService : CoreService,
               private router :Router,
               private translate : TranslateService) { }

   ngOnInit() {
      this.pageTitleService.setTitle("Courses");
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
     * getCoursesByType method is used to get the type of courses.
     */
   getCoursesByType (type) {
      let course = [];
      if(this.jsonResponse.courses && this.jsonResponse.courses.length>0) {
         for (let list of this.jsonResponse.courses){
            if(list.type == type){
               course.push(list);
            }
         }
         return course;
      }
   }

   /** 
     * getCoursesByPopularity method is used to get the popularity of courses.
     */
   getCoursesByPopularity (type) {
      let course = []; 
      if(this.jsonResponse.courses && this.jsonResponse.courses.length>0) {
         for (let list of this.jsonResponse.courses){
            if(list.popular == type){
               course.push(list);
            }
         }
        return course;
      }
   }

   //ViewAll method is used to show the course list.
   viewAll(){
      var first = location.pathname.split('/')[1];
      if(first == 'horizontal'){
         this.router.navigate(['/horizontal/courses/courses-list']);
      }else {
         this.router.navigate(['/courses/courses-list']);
      }
   }
}
