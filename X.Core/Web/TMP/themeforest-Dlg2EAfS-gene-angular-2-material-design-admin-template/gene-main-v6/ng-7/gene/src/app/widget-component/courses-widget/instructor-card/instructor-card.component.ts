import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-instructor-card',
  templateUrl: './instructor-card.component.html',
  styleUrls: ['./instructor-card.component.scss']
})

export class InstructorCardComponent implements OnInit {

	@Input() instruct : any =  [];

	constructor() { }

	ngOnInit() {
	}

}
