import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'ms-course-detail-description',
	templateUrl: './course-detail-description.component.html',
	styleUrls: ['./course-detail-description.component.scss']
})

export class CourseDetailDescriptionComponent implements OnInit {

	@Input() courseDetail : any = [];

	constructor(private translate : TranslateService) { }

	ngOnInit() {
	}

}
