import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { MatIconModule,
			MatButtonModule,
			MatTabsModule,
			MatCardModule,
			MatMenuModule,
			MatCheckboxModule,
			MatDividerModule,
			MatProgressBarModule,
         MatInputModule,      
			MatFormFieldModule,
			MatTableModule,
			MatListModule, 
			MatPaginatorModule,
			MatChipsModule,
			MatSortModule,
			MatSelectModule} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { RouterModule } from '@angular/router';
import { ChartsModule } from 'ng2-charts';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { AgmCoreModule } from '@agm/core';
import { EasyPieChartModule } from 'ng2modules-easypiechart';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { DashboardRoutes } from './dashboard.routing';

import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { WidgetComponentModule } from '../widget-component/widget-component.module';
import { SaasComponent } from './saas/saas.component';
import { CrmComponent } from './crm/crm.component';

@NgModule({
	declarations: [
		SaasComponent,
		CrmComponent
	],
	imports: [
		CommonModule,
		MatTableModule,
		MatSelectModule,
		FlexLayoutModule,
		WidgetComponentModule,
		EasyPieChartModule,
		MatPaginatorModule,
		MatChipsModule,
      TranslateModule,
      PerfectScrollbarModule,
		RouterModule.forChild(DashboardRoutes),
		MatIconModule,
		MatButtonModule,
		MatTabsModule,
		MatCardModule,
		MatMenuModule,
		MatListModule,
		MatCheckboxModule,
		MatDividerModule,
		ChartsModule,
		NgxDatatableModule,
		MatProgressBarModule,
		MatInputModule,
		MatFormFieldModule,
		FormsModule,
		ReactiveFormsModule,
		MatSortModule,
		AgmCoreModule.forRoot({apiKey: 'AIzaSyD4y2luRxfM8Q8yKHSLdOOdNpkiilVhD9k'})
	]
})
export class DashboardModule { }
