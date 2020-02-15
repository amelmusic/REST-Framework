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
import { TourMatMenuModule } from 'ngx-tour-md-menu';
import { ToastrModule } from 'ngx-toastr';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFireModule } from '@angular/fire';
import { MatSlideToggleModule,MatButtonModule, MatBadgeModule, MatCardModule, MatMenuModule, MatToolbarModule, MatIconModule, MatInputModule, MatDatepickerModule, MatNativeDateModule, MatProgressSpinnerModule,
			MatTableModule, MatExpansionModule, MatSelectModule, MatSnackBarModule, MatTooltipModule, MatChipsModule, MatListModule, MatSidenavModule, 
			MatTabsModule, MatProgressBarModule,MatCheckboxModule, MatSliderModule,MatRadioModule,MatDialogModule,MatGridListModule
} from '@angular/material';
import { RoutingModule } from "./app-routing.module";
import { LoadingBarRouterModule } from '@ngx-loading-bar/router';
import { LoadingBarModule } from '@ngx-loading-bar/core';
import { AuthService } from './service/auth-service/auth.service';
import { PageTitleService } from './core/page-title/page-title.service';
import { GeneAppComponent} from './app.component';
import { MainComponent }   from './main/main.component';
import { MenuToggleModule } from './core/menu/menu-toggle.module';
import { MenuItems } from './core/menu/menu-items/menu-items';
import { AuthGuard } from './core/guards/auth.guard';
import { WidgetComponentModule } from './widget-component/widget-component.module';
import { SideBarComponent } from './Shared/side-bar/side-bar.component';
import { FooterComponent } from './Shared/footer/footer.component';

export const firebaseConfig = {
	apiKey: "AIzaSyCE0po6Q8jGuBEds-A903KEU4U6Cerojzo",
	authDomain: "gene-eaeef.firebaseapp.com",
	databaseURL: "https://gene-eaeef.firebaseio.com",
	projectId: "gene-eaeef",
	storageBucket: "gene-eaeef.appspot.com",
	messagingSenderId: "81833823583",
	appId: "1:81833823583:web:581d7ddd8cfa93a4"
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
		RoutingModule,
		FlexLayoutModule,
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
		SideBarComponent, FooterComponent
	],
	bootstrap: [GeneAppComponent],
	providers: [
		MenuItems,
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
