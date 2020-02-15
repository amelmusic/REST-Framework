import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule, 
			MatButtonModule,
			MatProgressBarModule,
			MatIconModule,
			MatFormFieldModule,
			MatInputModule
		} from '@angular/material';
import { FormsModule} from'@angular/forms';
import { DragulaModule } from 'ng2-dragula';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { TaskBoardComponent } from './task-board/task-board.component';
import { TaskBoardRoutes } from './task-board.routing';

@NgModule({
	declarations: [TaskBoardComponent],
	imports: [
		CommonModule,
		MatCardModule,
		MatIconModule,
		MatButtonModule,
		MatProgressBarModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		FlexLayoutModule,
		TranslateModule,
		DragulaModule.forRoot(),
		RouterModule.forChild(TaskBoardRoutes)
	]
})

export class TaskBoardModule { }
