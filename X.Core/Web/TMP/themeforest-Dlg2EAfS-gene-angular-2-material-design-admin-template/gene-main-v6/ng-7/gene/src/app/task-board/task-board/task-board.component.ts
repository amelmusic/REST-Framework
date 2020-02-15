import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-task-board',
  templateUrl: './task-board.component.html',
  styleUrls: ['./task-board.component.scss']
})
export class TaskBoardComponent implements OnInit {
 
 	title   : string ;
 	message : string ;

	allTaskBoard : any [] = [
		{
			title : "Responsive",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "100",
			date : "May 17"
 		},
 		{
			title : "Checklist",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "0",
			date : "Apr 11"
 		},
 		{
			title : "Angular 7",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "50",
			date : "Aug 22"
 		}
	]

	todoTaskBoard : any = [
		{
			title : "Assessment",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "100",
			date : "Jun 19"
 		},
 		{
			title : "QA Testing",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "30",
			date : "May 17"
 		},
 		{
			title : "Navigation",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "0",
			date : "Apr 11"
 		}
	]

	doingTaskBoard : any = [
		{
			title : "Fields",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "70",
			date : "Aug 22"
 		},
 		{
			title : "Schedule",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "98",
			date : "Jun 19"
 		},
 		{
			title : "Budget",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "0",
			date : "May 17"
 		}
	]

	doneTaskBoard : any = [
		{
			title : "Material Design",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "0",
			date : "Apr 11"
 		},
 		{
			title : "Task board",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "68",
			date : "Aug 22"
 		},
 		{
			title : "Unit tests",
			message : "Team",
			taskboard_image : [ "assets/img/user-1.jpg","assets/img/user-2.jpg","assets/img/user-3.jpg","assets/img/user-4.jpg","assets/img/user-5.jpg"],
			value: "100",
			date : "Jun 19"
 		}
 	]

	constructor(private pageTitle : PageTitleService, private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitle.setTitle("Task Board");
	}

	/**
	  * onCreate Method is used to create new Task Board.
	  */
	onCreate() {
		if(this.message && this.title != "") {
			let create = {
				image: "assets/img/user-4.jpg",
				date : "8 May",
				title: this.title,
				message : this.message
			}
			this.allTaskBoard.push(create);
			this.title = "";
			this.message = "";
		}
	}


}
