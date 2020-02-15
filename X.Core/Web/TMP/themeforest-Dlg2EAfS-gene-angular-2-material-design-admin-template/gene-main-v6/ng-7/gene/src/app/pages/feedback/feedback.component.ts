import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss']
})
export class FeedbackComponent implements OnInit {

	ideaCount 	: number = 0;
	commentCount : number = 0;

	feedbackList : any [] = [
		{
			type: "comment",
			image : "assets/img/user-1.jpg",
			name : "Ramona Smith",
			comment :"You Will Never Believe These Bizarre Truth Of",
			feedback :"Lorem ipsum dolor sit amet consectetur",
			time : "8 hours ago"
		},
		{
			type : "idea",
			image : "assets/img/user-2.jpg",
			name : "Kristen McFalls",
			comment :"This Is Why This Year Will Be The Year Of",
			feedback : "Adipisicing elit, incididunt ut labore",
			time : "5 hour ago"
		},
		{
			type: "comment",
			image : "assets/img/user-3.jpg",
			name : "Antoine Downs",
			comment :"The 15 Secrets You Will Never Know About",
			feedback :"Velit esse cillum dolore eu fugiat nulla",
			time : "4 hours ago"
		},
		{
			type : "idea",
			image : "assets/img/user-4.jpg",
			name : "Patricia Gonzalez",
			comment :"You Will Never Believe These Bizarre Truth Of",
			feedback : "Sunt in culpa qui officia deserunt mollit anim",
			time : "10 hour ago"
		},
		{
			type: "comment",
			image : "assets/img/user-5.jpg",
			name : "Shirley Johnson",
			comment :"Ten Important Facts That You Should Know About",
			feedback :"Excepteur sint occaecat cupidatat non",
			time : "15 hours ago"
		},
		{
			type : "idea",
			image : "assets/img/user-1.jpg",
			name : "Gladys Rice",
			comment :"How Is Going To Change Your Business Strategies",
			feedback : "Duis aute irure dolor in reprehenderit sint",
			time : "10 hour ago"
		},
		{
			type : "idea",
			image : "assets/img/user-3.jpg",
			name : "Patricia Mullins",
			comment :"Will Be A Thing Of The Past And Here's Why",
			feedback : "Officia deserunt mollit anim id est laborum",
			time : "10 hour ago"
		},
		{
			type : "idea",
			image : "assets/img/user-4.jpg",
			name : "Jennifer Lara",
			comment :"This template gives us new idea",
			feedback : "Lorem ipsum dolor sit amet consectetur",
			time : "10 hour ago"
		}
	]

	constructor(private pageTitleService: PageTitleService, private translate : TranslateService) { }

	ngOnInit() {
		this.feedbackCount();
		this.pageTitleService.setTitle("Feedback");
	}

	//feedbackCount method is used to count the feedback of idea and comment.
	feedbackCount (){
		for (let data of this.feedbackList){
			if((data.type) == 'comment'){
				this.commentCount = this.commentCount + 1;
			}
			else{
				this.ideaCount = this.ideaCount + 1;
			}
		}
	}

}
