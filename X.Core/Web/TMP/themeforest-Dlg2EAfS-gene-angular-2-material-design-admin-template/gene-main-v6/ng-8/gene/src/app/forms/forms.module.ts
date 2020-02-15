import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatInputModule,
			MatFormFieldModule,
			MatCardModule,
			MatButtonModule,
			MatIconModule,
			MatDividerModule,
			MatTabsModule,
			MatRadioModule,
			MatProgressBarModule} from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FileUploadModule } from 'ng2-file-upload/ng2-file-upload';
import { TreeModule } from 'angular-tree-component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { FormWizardComponent}  from './form-wizard/formwizard.component';
import { FormValidationComponent}  from './form-validation/formvalidation.component';
import { FormUploadComponent}  from './form-upload/formupload.component';
import { FormTreeComponent}  from './form-tree/formtree.component';
import { FormRoutes } from './forms.routing';

@NgModule({
	declarations: [
		FormWizardComponent,
		FormValidationComponent,
		FormUploadComponent,
		FormTreeComponent
	],
	imports: [
		CommonModule,
		RouterModule.forChild(FormRoutes),
		TreeModule.forRoot(),
		MatInputModule,
		MatFormFieldModule,
		MatCardModule,
		MatButtonModule,
		MatIconModule,
		MatDividerModule,
		MatTabsModule,
		MatRadioModule,
		MatProgressBarModule,
		FormsModule,
		ReactiveFormsModule,
		FileUploadModule,
		FlexLayoutModule,
		TranslateModule
	]
})
export class FormModule { }
