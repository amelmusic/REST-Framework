import { Routes } from '@angular/router';

import { ProjectsComponent } from './projects/projects.component';
import { ClientsComponent } from './clients/clients.component';
import { ReportsComponent } from './reports/reports.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';

export const crmRouters : Routes = [
	{
		path : '',
		redirectTo : 'projects',
		pathMatch : 'full'
	},
	{
		path : '',
		children : [
			{
				path: "projects",
				component : ProjectsComponent
			},
			{
				path: "clients",
				component : ClientsComponent
			},
			{
				path: "reports",
				component : ReportsComponent
			},
			{
				path: "project-detail",
				component : ProjectDetailComponent
			},
			{
				path: "project-detail/:id",
				component : ProjectDetailComponent
			}
		]
	}	
]