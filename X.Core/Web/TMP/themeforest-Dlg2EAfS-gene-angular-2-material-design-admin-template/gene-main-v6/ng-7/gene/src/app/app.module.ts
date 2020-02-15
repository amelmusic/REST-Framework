import { NgModule, CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import 'hammerjs';
import { BrowserModule} from '@angular/platform-browser';
import { BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule, Routes } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';

import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { TranslateModule, TranslateLoader} from '@ngx-translate/core';
import { TranslateHttpLoader} from '@ngx-translate/http-loader';
import { Ng5BreadcrumbModule, BreadcrumbService} from 'ng5-breadcrumb';
import { DeviceDetectorModule,DeviceDetectorService } from 'ngx-device-detector';
import { TourMatMenuModule } from 'ngx-tour-md-menu';
import { ToastrModule } from 'ngx-toastr';

import { AngularFireAuthModule } from 'angularfire2/auth';
import { AngularFireModule } from 'angularfire2'

import { MatSlideToggleModule,MatButtonModule, MatBadgeModule, MatCardModule, MatMenuModule, MatToolbarModule, MatIconModule, MatInputModule, MatDatepickerModule, MatNativeDateModule, MatProgressSpinnerModule,
			MatTableModule, MatExpansionModule, MatSelectModule, MatSnackBarModule, MatTooltipModule, MatChipsModule, MatListModule, MatSidenavModule, 
			MatTabsModule, MatProgressBarModule,MatCheckboxModule, MatSliderModule,MatRadioModule,MatDialogModule,MatGridListModule
} from '@angular/material';

import { RoutingModule } from "./app-routing.module";
import { LoadingBarRouterModule } from '@ngx-loading-bar/router';
import { LoadingBarModule } from '@ngx-loading-bar/core';

import { AuthService } from './service/auth-service/auth.service';
import { PageTitleService } from './core/page-title/page-title.service';
import { D3ChartService } from "./core/nvD3/nvD3.service";

import { GeneAppComponent} from './app.component';
import { MainComponent }   from './main/main.component';
import { MenuToggleModule } from './core/menu/menu-toggle.module';
import { MenuItems } from './core/menu/menu-items/menu-items';
import { AuthGuard } from './core/guards/auth.guard';
import { HorizontalMenuItems } from './core/menu/horizontal-menu-items/horizontal-menu-items';

import { WidgetComponentModule } from './widget-component/widget-component.module';
import { HorizontalLayoutComponent } from './horizontal-layout/horizontal-layout.component';
import { PagesModule } from './pages/pages.module';
import { SideBarComponent } from './Shared/side-bar/side-bar.component';

export const firebaseConfig = {
	apiKey				: "AIzaSyBO0CLP4fOA_kanqw1HQ2sDjEkyuK9lQ3o",
	authDomain			: "gene-ccf5f.firebaseapp.comm",
	databaseURL			: "https://gene-ccf5f.firebaseio.com",
	projectId			: "gene-ccf5fc",
	storageBucket		: "gene-ccf5f.appspot.com",
	messagingSenderId : "907778578362"
}

// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
   return new TranslateHttpLoader(http, 'assets/i18n/', '.json');
}

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		FormsModule,
		ReactiveFormsModule,
		DeviceDetectorModule.forRoot(),
		RoutingModule,
		FlexLayoutModule,
		PagesModule,
		NgbModalModule.forRoot(),
		Ng5BreadcrumbModule.forRoot(),
		TourMatMenuModule.forRoot(),
		PerfectScrollbarModule,
		MenuToggleModule,
      HttpClientModule,
      MatSlideToggleModule,
		TranslateModule.forRoot({
         loader: {
				provide: TranslateLoader,
				useFactory: HttpLoaderFactory,
				deps: [HttpClient]
         }
      }),
		AngularFireModule.initializeApp(firebaseConfig),
    	AngularFireAuthModule,
		MatButtonModule, 
		MatCardModule, 
		MatMenuModule, 
		MatToolbarModule, 
		MatIconModule, 
		MatBadgeModule,
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
		ToastrModule.forRoot(),
		WidgetComponentModule,
		LoadingBarRouterModule,
		LoadingBarRouterModule
	],
	declarations: [
		GeneAppComponent, 
		MainComponent, 
		HorizontalLayoutComponent, SideBarComponent
	],
	bootstrap: [GeneAppComponent],
	providers: [
		D3ChartService,
		MenuItems,
		HorizontalMenuItems,
		BreadcrumbService,
		PageTitleService,
		AuthService,
		{
			provide: PERFECT_SCROLLBAR_CONFIG,
			useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
		},
		AuthGuard
	],
   schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class GeneAppModule { }
