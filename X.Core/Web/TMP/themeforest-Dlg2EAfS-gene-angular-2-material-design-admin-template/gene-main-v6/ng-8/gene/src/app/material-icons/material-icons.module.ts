import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatIconModule, MatButtonModule, MatTabsModule, MatCardModule} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { MaterialIconComponent}  from './material-icons/icons.component';
import { IconsRoutes } from './material-icons.routing';

@NgModule({
	declarations: [
		MaterialIconComponent
	],
	imports: [
		CommonModule,
		RouterModule.forChild(IconsRoutes),
		MatIconModule,
		MatButtonModule,
		MatTabsModule,
		MatCardModule,
		FlexLayoutModule,
		TranslateModule
	]
})
export class MaterialIconsModule { }
