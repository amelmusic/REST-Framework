import { Component, OnInit } from '@angular/core';
import { CoreService } from '../../service/core/core.service';
import { Router,ActivatedRoute, Params } from '@angular/router';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core'

@Component({
  selector: 'ms-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.scss']
})

export class ProjectDetailComponent implements OnInit {

	projectDetails : any;
   Id             : any;
	projectGallaryConfig  = {"slidesToShow": 1,"fade": true, "slidesToScroll": 1,"arrows": false,"dots": false,"autoplay": true, "autoplaySpeed": 2000};

	constructor(private coreService : CoreService,
					private route: ActivatedRoute,
               private router: Router,
               private pageTitleService: PageTitleService,
               private translate : TranslateService) { }

	ngOnInit() {
      this.pageTitleService.setTitle("Project Detail");
		this.projectDetails = this.coreService.projectDetailsContent;
		this.route.params.subscribe(res => {
			this.Id = res.id;
			this.getProjectDetails();
      });
   }

   //getProjectDetails method is used to get the project details.
   public getProjectDetails() {
      this.Id = (this.Id) ? this.Id : 1;
      this.coreService.getProjectContent().
        subscribe(res => {this.getProjectDetailsResponse(res)});
   }

   //getProjectDetailsResponse method is used to get the response of project and then show the project details.
   public getProjectDetailsResponse(response) {
      for(let data of response)
      {
         if(data.id == this.Id) {
            this.projectDetails = data;
            break;
         }
      }
   }
}
