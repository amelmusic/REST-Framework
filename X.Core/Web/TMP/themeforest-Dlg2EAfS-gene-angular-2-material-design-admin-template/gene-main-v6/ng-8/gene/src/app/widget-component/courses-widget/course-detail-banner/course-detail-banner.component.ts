import { Component, OnInit, Input } from '@angular/core';
import { CoreService } from '../../../service/core/core.service'; 

@Component({
  selector: 'ms-course-detail-banner',
  templateUrl: './course-detail-banner.component.html',
  styleUrls: ['./course-detail-banner.component.scss']
})
export class CourseDetailBannerComponent implements OnInit {

	@Input() courseDetail : any = [];
	
	constructor(private coreService : CoreService) { }

	ngOnInit() {
	}

	/**
	  * onVideoPlayer method is used to open a video player dialog.
	  */
	onVideoPlayer(){
		this.coreService.videoPlayerDialog(this.courseDetail.demoVideoUrl);
	}

}
