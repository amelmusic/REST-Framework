import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatInputModule,
			MatFormFieldModule,
			MatCardModule,
			MatButtonModule,
			MatIconModule,
			MatPaginatorModule,
			MatDividerModule,
			MatCheckboxModule,
			MatTableModule,
			MatTabsModule,
			MatChipsModule,
			MatSortModule,
			MatSelectModule } from '@angular/material';
import { UserManagementRoutes } from './user-management.routing';
import { UserManageListComponent } from './user-manage-list/user-manage-list.component';
import { UserGridListComponent } from './user-grid-list/user-grid-list.component'
import { WidgetComponentModule } from '../widget-component/widget-component.module';

@NgModule({
	declarations: [ 
		UserManageListComponent,
		UserGridListComponent
	],
	imports: [
		CommonModule,
		FlexLayoutModule,
		RouterModule.forChild(UserManagementRoutes),
		MatInputModule,
		MatFormFieldModule,
		MatCardModule,
		MatButtonModule,
		MatIconModule,
		MatPaginatorModule,
		MatDividerModule,
		MatCheckboxModule,
		MatTableModule,
		MatTabsModule,
		MatChipsModule,
		MatSelectModule,
		WidgetComponentModule,
		MatSortModule
	]
})
export class UserManagementModule { }
