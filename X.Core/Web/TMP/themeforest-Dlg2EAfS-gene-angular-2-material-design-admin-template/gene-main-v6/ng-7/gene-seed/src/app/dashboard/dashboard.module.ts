import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard-v1/dashboard.component';
import { DashboardRoutes } from './dashboard.routing';

@NgModule({
	declarations: [
		DashboardComponent
	],
	imports: [
		CommonModule,
		FlexLayoutModule,
		RouterModule.forChild(DashboardRoutes),
	]
})

export class DashboardModule { }
