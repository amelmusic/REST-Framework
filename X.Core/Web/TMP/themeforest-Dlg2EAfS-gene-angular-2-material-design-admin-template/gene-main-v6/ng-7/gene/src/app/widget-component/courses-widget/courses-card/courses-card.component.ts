import { Component, OnInit, Input } from '@angular/core';
import { CoreService } from '../../../service/core/core.service';
import { Router } from '@angular/router';

@Component({
  selector: 'ms-courses-card',
  templateUrl: './courses-card.component.html',
  styleUrls: ['./courses-card.component.scss']
})
export class CoursesCardComponent implements OnInit {

	@Input() course;

	constructor( private coreService : CoreService,
					 public router : Router) { }

	ngOnInit() {
	}

	/**
	  *onVideoPlayer method is used to open a video player dialog.
	  */
	onVideoPlayer(){
		this.coreService.videoPlayerDialog(this.course.demoVideoUrl);
	}

	//to open the course detail page
	openCourseDetail(){
		var first = location.pathname.split('/')[1];
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/courses/course-detail']);
		}else {
			this.router.navigate(['/courses/course-detail']);
		}
	}

}
