import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule} from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule,
			MatIconModule,
			MatButtonModule,
			MatDividerModule,
			MatGridListModule,
			MatListModule,
			MatMenuModule,
			MatPaginatorModule,
			MatTabsModule,
			MatChipsModule,
			MatFormFieldModule,
			MatExpansionModule,
         MatCheckboxModule,
         MatRadioModule,
         MatSelectModule,
			MatInputModule
			} from '@angular/material';
import {ReactiveFormsModule, FormsModule} from '@angular/forms';
import { BarRatingModule } from "ngx-bar-rating";
import { CardModule } from 'ngx-card/ngx-card';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { CoursesRoutes } from './courses.routing';
import { PaymentComponent } from './payment/payment.component';
import { SigninComponent } from './signin/signin.component';
import { CoursesDetailComponent } from './courses-detail/courses-detail.component';
import { CourseListComponent } from './course-list/course-list.component';
import { WidgetComponentModule } from '../widget-component/widget-component.module';

@NgModule({
	declarations: [
		CoursesDetailComponent,
		CourseListComponent,
		PaymentComponent, 
		SigninComponent
	],
	imports: [
		CommonModule,
		ReactiveFormsModule,
		FormsModule,
		RouterModule.forChild(CoursesRoutes),
		FlexLayoutModule,
		MatCardModule,
		CardModule,
		MatIconModule,
		MatButtonModule,
		MatDividerModule,
		MatGridListModule,
		MatListModule,
		MatMenuModule,
		MatPaginatorModule,
		MatTabsModule,
		MatChipsModule,
		MatFormFieldModule,
		MatExpansionModule,
      MatInputModule,
      MatRadioModule,
      MatSelectModule,
      MatCheckboxModule,
      BarRatingModule,
    	TranslateModule,
    	WidgetComponentModule
	]
})
export class CoursesModule { }
