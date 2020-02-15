import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-course-detail-learn',
  templateUrl: './course-detail-learn.component.html',
  styleUrls: ['./course-detail-learn.component.scss']
})

export class CourseDetailLearnComponent implements OnInit {

	@Input() courseDetail : any = [];

	constructor(private translate : TranslateService) { }

	ngOnInit() {
	}

}
