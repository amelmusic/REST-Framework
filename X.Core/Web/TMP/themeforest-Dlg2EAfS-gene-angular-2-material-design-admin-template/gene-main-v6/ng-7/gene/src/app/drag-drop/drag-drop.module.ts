import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DragulaModule } from 'ng2-dragula';
import { SortablejsModule, SortablejsOptions} from "angular-sortablejs";
import { MatCardModule,
			MatIconModule,
			MatButtonModule,
			MatListModule,
			MatCheckboxModule,
			MatToolbarModule} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { DragDropRoutes } from './drag-drop.routing';
import { DragulaDemoComponent } from './dragula/dragula.component';
import { SortableDemoComponent } from './sortablejs/sortable.component';

const sortablejsConfig: SortablejsOptions = {
	animation: 300
};

@NgModule({
	declarations: [
		DragulaDemoComponent, 
		SortableDemoComponent
	],

	imports: [
		CommonModule,
		RouterModule.forChild(DragDropRoutes),
		DragulaModule.forRoot(),
		SortablejsModule,
		FlexLayoutModule,
		MatIconModule,
		MatCardModule,
		MatIconModule,
		MatListModule,
		MatCheckboxModule,
		MatToolbarModule,
    	TranslateModule
	]
})

export class DragDropModule { }
