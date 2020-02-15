import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule,
			MatButtonModule,
			MatIconModule,
			MatDialogModule,
			MatFormFieldModule,
			MatSelectModule,
			MatMenuModule,
			MatDividerModule,
			MatSnackBarModule,
			MatInputModule,
			MatChipsModule,
			MatListModule,
			MatExpansionModule
		} from '@angular/material';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';

import { SlickCarouselModule } from 'ngx-slick-carousel';
import { LanguageDropDownComponent } from './global/language-drop-down/language-drop-down.component';

@NgModule({
	declarations: [
		LanguageDropDownComponent
	],
	imports: [
		RouterModule,
		CommonModule,
		MatCardModule,
		FlexLayoutModule,
		MatInputModule,
		MatButtonModule,
		MatIconModule,
		MatExpansionModule,
		MatDialogModule,
		MatFormFieldModule,
		MatSelectModule,
		MatMenuModule,
		MatDividerModule,
		FormsModule,
		ReactiveFormsModule,
		TextMaskModule,
		MatSnackBarModule,
		SlickCarouselModule,
		TranslateModule,
		MatChipsModule,
		MatListModule

	],
	exports: [
		LanguageDropDownComponent
	]
})

export class WidgetComponentModule { }
