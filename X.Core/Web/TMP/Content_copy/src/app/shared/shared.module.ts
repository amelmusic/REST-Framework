import { NgModule, LOCALE_ID, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
	MatSlideToggleModule, MatButtonModule, MatBadgeModule, MatCardModule, MatMenuModule, MatToolbarModule, MatIconModule, MatInputModule, MatDatepickerModule, MatProgressSpinnerModule,
	MatTableModule, MatExpansionModule, MatSelectModule, MatSnackBarModule, MatTooltipModule, MatChipsModule, MatListModule, MatSidenavModule,
	MatTabsModule, MatProgressBarModule, MatCheckboxModule, MatSliderModule, MatRadioModule, MatDialogModule, MatGridListModule, MatAutocompleteModule, MatSortModule, MatPaginatorModule, MatStepperModule
} from '@angular/material';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { registerLocaleData } from '@angular/common';
import localeBs from '@angular/common/locales/bs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormlyModule, FORMLY_CONFIG } from '@ngx-formly/core';
import { FormlyMaterialModule } from '@ngx-formly/material';
import { AutoCompleteComponent } from './components/auto-complete/auto-complete.component';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { FormlyHeaderComponent } from './components/formly/formly-header/formly-header.component';
import { FormlyAutocompleteComponent } from './components/formly/formly-autocomplete/formly-autocomplete.component';
import { FormlyRepeatComponent } from './components/formly/formly-repeat/formly-repeat.component';
import { FormlyWrapperAddonsComponent } from './components/formly/formly-wrapper-addons/formly-wrapper-addons.component';
import { FormlyButtonComponent } from './components/formly/formly-button/formly-button.component';
import { CellLocalizerDirective } from './directives/cell-localizer.directive';
import { addonsExtension } from './components/formly/addons.extension';
import { registerTranslateExtension } from './services/translate-formly-extension.service';
import { XCoreErrorHandlerService } from './services/xcore-error-handler.service';
import { ErrorComponent } from './components/error/error.component';
import { FormlyMatDatepickerModule } from '@ngx-formly/material/datepicker';
import { DatePickerTypeXCoreComponent } from './components/formly/date-picker-type-xcore/date-picker-type-xcore.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { UploadComponent } from './components/upload/upload.component';
import { FormlyUploadComponent } from './components/formly/formly-upload/formly-upload.component';
import { FileViewerComponent } from './components/file-viewer/file-viewer.component';
import { FileViewerDialogComponent } from './components/file-viewer/file-viewer-dialog/file-viewer-dialog.component';

registerLocaleData(localeBs);

@NgModule({
	declarations: [
		AutoCompleteComponent,
		ConfirmationDialogComponent,
		FormlyHeaderComponent,
		FormlyAutocompleteComponent,
		FormlyRepeatComponent,
		FormlyWrapperAddonsComponent,
		FormlyButtonComponent,
		CellLocalizerDirective,
		ErrorComponent,
		DatePickerTypeXCoreComponent,
		SpinnerComponent,
		UploadComponent,
		FormlyUploadComponent,
		FileViewerComponent,
		FileViewerDialogComponent],
	imports: [
		CommonModule,
		TranslateModule,
		MatSlideToggleModule,
		MatButtonModule,
		MatCardModule,
		MatMenuModule,
		MatToolbarModule,
		MatIconModule,
		MatBadgeModule,
		MatInputModule,
		MatDatepickerModule,
		MatMomentDateModule,
		MatProgressSpinnerModule,
		MatTableModule,
		MatExpansionModule,
		MatSelectModule,
		MatSnackBarModule,
		MatTooltipModule,
		MatChipsModule,
		MatListModule,
		MatSidenavModule,
		MatTabsModule,
		MatProgressBarModule,
		MatCheckboxModule,
		MatSliderModule,
		MatRadioModule,
		MatDialogModule,
		MatGridListModule,
		MatAutocompleteModule,
		MatSortModule,
		MatPaginatorModule,
		MatStepperModule,

		FlexLayoutModule,
		FormsModule,
		ReactiveFormsModule,
		FormlyMatDatepickerModule,
		FormlyModule.forChild({
			types: [{
				name: 'header',
				component: FormlyHeaderComponent,
			}, {
				name: 'autocomplete',
				component: FormlyAutocompleteComponent,
			},
			{ name: 'repeat', component: FormlyRepeatComponent },
			{ name: 'upload', component: FormlyUploadComponent },
			{
				name: 'button',
				component: FormlyButtonComponent,
				defaultOptions: {
					templateOptions: {
						type: 'button',
					},
				},
			}],
			wrappers: [
				{ name: 'addons', component: FormlyWrapperAddonsComponent },
			],
			extensions: [
				{ name: 'addons', extension: { onPopulate: addonsExtension } },
			]
		}),
		FormlyMaterialModule,
	],
	entryComponents: [FileViewerDialogComponent],
	exports: [
		TranslateModule,
		MatSlideToggleModule,
		MatButtonModule,
		MatCardModule,
		MatMenuModule,
		MatToolbarModule,
		MatIconModule,
		MatBadgeModule,
		MatInputModule,
		MatDatepickerModule,
		MatMomentDateModule,
		MatProgressSpinnerModule,
		MatTableModule,
		MatExpansionModule,
		MatSelectModule,
		MatSnackBarModule,
		MatTooltipModule,
		MatChipsModule,
		MatListModule,
		MatSidenavModule,
		MatTabsModule,
		MatProgressBarModule,
		MatCheckboxModule,
		MatSliderModule,
		MatRadioModule,
		MatDialogModule,
		MatGridListModule,
		MatAutocompleteModule,
		MatSortModule,
		MatPaginatorModule,
		FormlyMatDatepickerModule,
		MatStepperModule,

		FlexLayoutModule,
		FormsModule,
		ReactiveFormsModule,
		FormlyModule,
		FormlyMaterialModule,
		AutoCompleteComponent,
		ConfirmationDialogComponent,
		FormlyHeaderComponent,
		FormlyAutocompleteComponent,
		FormlyRepeatComponent,
		FormlyWrapperAddonsComponent,
		FormlyButtonComponent,
		CellLocalizerDirective,
		ErrorComponent,
		DatePickerTypeXCoreComponent,
		SpinnerComponent,
		UploadComponent,
		FormlyUploadComponent,
		FileViewerComponent,
		FileViewerDialogComponent],
	providers: [{
		provide: LOCALE_ID,
		deps: [],
		useFactory: (fact) => {
			return localStorage.getItem('core.language') ? localStorage.getItem('core.language') : 'bs';
		}
	},
	{
		provide: ErrorHandler,
		useClass: XCoreErrorHandlerService
	}, { provide: FORMLY_CONFIG, multi: true, useFactory: registerTranslateExtension, deps: [TranslateService] }]
})
export class SharedModule { }
