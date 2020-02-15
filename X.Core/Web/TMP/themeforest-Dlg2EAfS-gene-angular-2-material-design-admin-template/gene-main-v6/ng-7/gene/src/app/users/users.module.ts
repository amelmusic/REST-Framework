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
			MatSelectModule } from '@angular/material';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserListComponent } from './user-list/userlist.component';
import { UserProfileV2Component } from './user-profile-v2/user-profile-v2.component';
import { WidgetComponentModule } from '../widget-component/widget-component.module';
import { UserRoutes } from './users.routing';

@NgModule({
declarations: 
	[
		UserListComponent,
		UserProfileComponent,
		UserProfileV2Component
   ],
	imports: [
		CommonModule,
		FlexLayoutModule,
		RouterModule.forChild(UserRoutes),
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
		TranslateModule
	]
})
export class UsersModule { }
