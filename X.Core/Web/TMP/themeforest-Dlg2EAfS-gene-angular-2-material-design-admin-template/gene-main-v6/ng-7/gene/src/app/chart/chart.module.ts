import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ChartsModule } from 'ng2-charts';
import { EasyPieChartModule } from 'ng2modules-easypiechart';
import { MatIconModule,
			MatButtonModule,
			MatCardModule,
			MatMenuModule,
			MatDividerModule} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { ChartComponent } from './ng2-charts/chart.component';
import { EasyPieChartComponent } from './easy-pie-chart/easy-pie-chart.component';
import { ChartRoutes } from './chart.routing';
import { WidgetComponentModule } from '../widget-component/widget-component.module';


@NgModule({
	declarations: [EasyPieChartComponent, ChartComponent],
	imports: [
		CommonModule,
		ChartsModule,
		EasyPieChartModule,
		WidgetComponentModule,
		MatIconModule,
		MatButtonModule,
		MatCardModule,
		MatDividerModule,
		MatMenuModule,
		FlexLayoutModule,
    	TranslateModule,
    	RouterModule.forChild(ChartRoutes)
	]
})
export class ChartModule { }
