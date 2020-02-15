import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule, 
			MatCardModule, 
			MatMenuModule, 
			MatToolbarModule, 
			MatIconModule, 
			MatInputModule, 
			MatDatepickerModule, 
			MatNativeDateModule, 
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
			MatButtonToggleModule
		} from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {ColorPickerModule} from 'ngx-color-picker';

import { ComponentsRoutes } from './components.routing';
import { ButtonsComponent } from './buttons/buttons.component';
import { CheckboxComponent } from './checkbox/checkbox.component';
import { CardsComponent } from './cards/cards.component';
import { ColorpickerComponent } from './colorpicker/colorpicker.component';
import { DatepickerComponent } from './datepicker/datepicker.component';
import { DialogOverviewComponent,DemoDialog } from './dialog/dialog.component';
import { GridListComponent } from './grid-list/gridlist.component';
import { InputComponent } from './input/input.component';
import { ListOverviewComponent } from './list/list.component';
import { MenuOverviewComponent } from './menu/menu.component';
import { ProgressComponent } from './progress/progress.component';
import { RadioComponent } from './radio/radio.component';
import { SelectComponent } from './select/select.component';
import { SliderOverviewComponent } from './slider/slider.component';
import { SnackbarOverviewComponent } from './snackbar/snackbar.component';
import { TabsComponent } from './tabs/tabs.component';
import { TooltipOverviewComponent } from './tooltip/tooltip.component';
import { ToolbarComponent } from './toolbar/toolbar.component';

@NgModule({
	declarations: [
		ButtonsComponent,
		CheckboxComponent,
		CardsComponent,
		ColorpickerComponent,
		DatepickerComponent,
		DialogOverviewComponent,
		InputComponent,
		ListOverviewComponent,
		SelectComponent,
		TooltipOverviewComponent,
		ToolbarComponent,
		TabsComponent,
		SnackbarOverviewComponent,
		TabsComponent,
		SliderOverviewComponent,
		RadioComponent,
		ProgressComponent,
		GridListComponent,
		DemoDialog,
		MenuOverviewComponent
	],

	imports: [
		CommonModule,
		FormsModule,
		RouterModule.forChild(ComponentsRoutes),
		ColorPickerModule,
		MatButtonModule, 
		MatCardModule, 
		MatMenuModule, 
		MatToolbarModule, 
		MatIconModule, 
		MatInputModule,
		MatButtonToggleModule, 
		MatDatepickerModule, 
		MatNativeDateModule, 
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
		ReactiveFormsModule,
		FlexLayoutModule,
    	TranslateModule
	],
	entryComponents : [
		DemoDialog
	]
})
export class ComponentsModule { }
