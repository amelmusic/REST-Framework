import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';
import { CoreService } from '../../service/core/core.service';

@Component({
  selector: 'ms-task-board',
  templateUrl: './task-board.component.html',
  styleUrls: ['./task-board.component.scss']
})
export class TaskBoardComponent implements OnInit {
 
 	title  			 : string;
 	message 		 : string;
 	taskboardContent : any;

	constructor( private pageTitle : PageTitleService, 
				 private translate : TranslateService,
				 public coreService : CoreService) { }

	ngOnInit() {
		this.pageTitle.setTitle("Task Board");
		this.coreService.getTaskboardContent().
            subscribe( res => { this.taskboardContent = res },
                       err => console.log(err),
                       ()  => this.taskboardContent
                    );
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
			this.taskboardContent.allTaskBoard.push(create);
			this.title = "";
			this.message = "";
		}
	}
}
