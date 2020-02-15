import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule} from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule,
			MatIconModule,
			MatButtonModule,
			MatDividerModule,
			MatGridListModule,
			MatListModule,
			MatMenuModule,
			MatPaginatorModule,
			MatTabsModule,
			MatChipsModule,
			MatFormFieldModule,
			MatExpansionModule,
			MatInputModule,
			MatSlideToggleModule
		} from '@angular/material';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { MediaComponent } from './media/media.component';
import { MediaV2Component } from './mediaV2/mediaV2.component';
import { PricingComponent } from './pricing/pricing.component';
import { BlankComponent } from './blank/blank.component';
import { FaqComponent } from './faq/faq.component';
import { AboutComponent } from './about/about.component';
import { TimelineComponent } from './timeline/timeline.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { PagesRoutes } from './pages.routing';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { CommingsoonComponent } from './commingsoon/commingsoon.component';
import { Pricing1Component } from './pricing1/pricing1.component';
import { ContactComponent } from './contact/contact.component';
import { SearchComponent } from './search/search.component';

@NgModule({
	declarations: [
		MediaComponent, 
		MediaV2Component, 
		PricingComponent,
		BlankComponent,
		FeedbackComponent,
		TimelineComponent,
		FaqComponent,
		AboutComponent,
		MaintenanceComponent,
		CommingsoonComponent,
		Pricing1Component,
		ContactComponent,
		SearchComponent
	],
	imports: [
		CommonModule,
		FlexLayoutModule,
		MatCardModule,
		MatPaginatorModule,
		MatInputModule,
		MatExpansionModule,
		MatFormFieldModule,
		MatTabsModule,
		MatChipsModule,
		MatIconModule,
		MatButtonModule,
		MatDividerModule,
		MatGridListModule,
		MatSlideToggleModule,
		MatListModule,
		MatMenuModule,
		RouterModule.forChild(PagesRoutes),
		TranslateModule
	]
})
export class PagesModule { }
