import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
			MatInputModule,
			MatCheckboxModule,
			MatSelectModule,
			MatOptionModule,
			MatDialogModule,
			MatPaginatorModule} from '@angular/material';
import { FormsModule} from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { InboxComponent } from './inbox/inbox.component';

import { inboxRoutes } from './inbox.routing';
import { MailService } from '../service/mail/mail.service';

@NgModule({
	declarations: [InboxComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(inboxRoutes),
		MatSidenavModule,
		MatIconModule,
		MatButtonModule,
		MatMenuModule,
		MatCardModule,
		MatToolbarModule,
		MatDividerModule,
		MatListModule,
		MatFormFieldModule,
		MatInputModule,
		MatCheckboxModule,
		FormsModule,
		MatSelectModule,
		MatOptionModule,
		MatDialogModule,
		FlexLayoutModule,
		MatPaginatorModule
	],
	providers: [
		MailService
	]
})
export class InboxModule { }
