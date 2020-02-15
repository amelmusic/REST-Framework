import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { MatInputModule,
			MatFormFieldModule,
			MatCardModule,
			MatButtonModule,
			MatIconModule,
			MatDividerModule} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { TablesRoutes } from './tables.routing';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { FullscreenTableComponent}  from './table-fullscreen/table-fullscreen.component';
import { EditingTableComponent}  from './table-editing/table-editing.component';
import { FilterTableComponent}  from './table-filter/table-filter.component';
import { PagingTableComponent}  from './table-paging/table-paging.component';
import { SortingTableComponent}  from './table-sorting/table-sorting.component';
import { PinningTableComponent}  from './table-pinning/table-pinning.component';
import { SelectionTableComponent}  from './table-selection/table-selection.component';
import { ResponsiveTableComponent}  from './table-responsive/table-responsive.component';


@NgModule({
	declarations: [
		FullscreenTableComponent,
		EditingTableComponent,
		FilterTableComponent,
		PagingTableComponent,
		SortingTableComponent,
		PinningTableComponent,
		SelectionTableComponent,
		ResponsiveTableComponent
	],
	imports: [
		CommonModule,
		RouterModule.forChild(TablesRoutes),
		NgxDatatableModule,
		MatInputModule,
		MatFormFieldModule,
		MatCardModule,
		MatIconModule,
		MatButtonModule,
		MatDividerModule,
		FlexLayoutModule,
		TranslateModule
	]
})
export class TablesModule { }
