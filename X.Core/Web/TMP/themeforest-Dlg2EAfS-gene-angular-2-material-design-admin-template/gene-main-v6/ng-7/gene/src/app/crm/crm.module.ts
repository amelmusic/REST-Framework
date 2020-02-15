import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { MatIconModule,
			MatButtonModule,
			MatTabsModule,
			MatCardModule,
			MatMenuModule,
         MatCheckboxModule,         
			MatTableModule,
			MatDividerModule,
         MatPaginatorModule,
			MatProgressBarModule,
         MatInputModule,      
         MatOptionModule,      
         MatSelectModule,
         MatChipsModule,    
			MatExpansionModule,  
			MatFormFieldModule,
			MatListModule} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ChartsModule } from 'ng2-charts';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SlickCarouselModule } from 'ngx-slick-carousel';

import { ProjectsComponent } from './projects/projects.component';
import { ClientsComponent } from './clients/clients.component';
import { ReportsComponent } from './reports/reports.component';
import { WidgetComponentModule } from '../widget-component/widget-component.module';
import { crmRouters } from './crm.routing';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

@NgModule({
	declarations: [
		ProjectsComponent, 
		ClientsComponent, 
		ReportsComponent, ProjectDetailComponent
	],
	imports: [
		CommonModule,
      RouterModule.forChild(crmRouters),
      MatIconModule,
		MatButtonModule,
		MatTabsModule,
      MatCardModule,      
		MatTableModule,
		MatMenuModule,
		MatListModule,
		ChartsModule,
		MatCheckboxModule,
      MatDividerModule, 
      MatPaginatorModule,     
		MatProgressBarModule,
		MatInputModule,
      MatFormFieldModule,
      PerfectScrollbarModule,
      MatExpansionModule,
		NgxDatatableModule,
      FlexLayoutModule,
      MatChipsModule,         
      MatOptionModule,
      MatSelectModule,
      SlickCarouselModule,
      TranslateModule,
      WidgetComponentModule
	]
})

export class CrmModule { }
