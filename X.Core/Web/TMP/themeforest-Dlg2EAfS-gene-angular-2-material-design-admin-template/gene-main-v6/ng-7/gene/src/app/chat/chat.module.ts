import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { chatRoutes } from './chat.routing';
import { RouterModule } from '@angular/router';
import { MatSidenavModule,
			MatIconModule,
			MatButtonModule,
			MatMenuModule,
			MatCardModule,
			MatToolbarModule,
			MatDividerModule,
			MatListModule,
			MatFormFieldModule,
			MatInputModule} from '@angular/material';
import { FormsModule } from '@angular/forms';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { FlexLayoutModule} from '@angular/flex-layout';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { ChatComponent } from './chat/chat.component';

@NgModule({
	declarations: [ChatComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(chatRoutes),
		MatIconModule,
		MatButtonModule,
		MatMenuModule,
		MatCardModule,
		MatToolbarModule,
		MatDividerModule,
		MatSidenavModule,
		MatListModule,
		MatFormFieldModule,
		FormsModule,
		PerfectScrollbarModule,
		MatInputModule,
		FlexLayoutModule,
    	TranslateModule
	]
})
export class ChatModule { }
