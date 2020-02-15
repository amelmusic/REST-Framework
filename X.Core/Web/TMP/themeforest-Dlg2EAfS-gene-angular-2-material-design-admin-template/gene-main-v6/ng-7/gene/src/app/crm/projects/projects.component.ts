import { Component, OnInit, ViewChild} from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { Router } from '@angular/router';
import { CoreService } from '../../service/core/core.service';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core'

@Component({
	selector: 'ms-projects',
	templateUrl: './projects.component.html',
	styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {

	showType : string = 'grid';

	@ViewChild(MatPaginator) paginator: MatPaginator;

	projectContent : any;

	liveTradeSource;

	displayedProjectIdColumns : string [] = ['id', 'name','budget', 'duration', 'client', 'team_image','status', 'deadline'];

	constructor(private router : Router,
					private coreService : CoreService,
					private pageTitleService: PageTitleService,
					private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Projects");
		this.showProjectData();
	}

	/**
	  * projectShowType method is used to select the show type of project.
	  */
	projectShowType(type) {
		this.showType = type;
		if(type=='list'){
			document.getElementById('list').classList.add("active");
			document.getElementById('grid').classList.remove('active');
			this.showProjectData();
		}
		else{
			document.getElementById('grid').classList.add("active");
			document.getElementById('list').classList.remove('active');
		}
	}

	/**
	  * projectDetails method is used to show the project details.
	  */
	projectDetails(content){

		var first = location.pathname.split('/')[1];
		console.log(first);
		if(first == 'horizontal'){
			this.router.navigate(['/horizontal/crm/project-detail',content.id]);
			this.coreService.projectDetailsContent = content;
		}
		else {
			this.router.navigate(['/crm/project-detail',content.id]);
			this.coreService.projectDetailsContent = content;
		}
	}

	/**
	  * showProjectData method is used to get the project data and set the pagination in project list.
	  */
	showProjectData(){
		this.coreService.getProjectContent().
			subscribe(  res => {this.projectContent = res},
							err =>console.log(err),
							() =>  this.liveTradeSource = new MatTableDataSource<any>(this.projectContent)
			);

		setTimeout(()=>{
			if(this.paginator){
				this.liveTradeSource.paginator = this.paginator;
			}
		},1000)
	}
}
