import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-research-interests',
  templateUrl: './research-interests.component.html',
  styleUrls: ['./research-interests.component.scss']
})
export class ResearchInterestsComponent implements OnInit {

	@Input() researchInterest;

	constructor(private translate : TranslateService) { }

	ngOnInit() {
	}

}
