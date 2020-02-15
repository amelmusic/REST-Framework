import { Component, OnInit, Input } from '@angular/core';
import { CoreService } from '../../../service/core/core.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-course-detail-overview',
  templateUrl: './course-detail-overview.component.html',
  styleUrls: ['./course-detail-overview.component.scss']
})

export class CourseDetailOverviewComponent implements OnInit {

	@Input() courseTopics : any;
	
	constructor(private coreService : CoreService,
		private translate : TranslateService) { }

	ngOnInit() {
	}

	/** 
	  * onVideoPlayer method is used to open a video player dialog.
	  */
	onVideoPlayer(videoUrl) {
		this.coreService.videoPlayerDialog(videoUrl);
	}

}
