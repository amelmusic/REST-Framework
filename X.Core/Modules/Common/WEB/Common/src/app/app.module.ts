import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import 'hammerjs';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TourMatMenuModule } from 'ngx-tour-md-menu';
import { ToastrModule } from 'ngx-toastr';
import { RoutingModule } from "./app-routing.module";
import { LoadingBarRouterModule } from '@ngx-loading-bar/router';
import { LoadingBarModule } from '@ngx-loading-bar/core';
import { PageTitleService } from './shared/services/page-title.service';
import { XCoreAppComponent } from './app.component';
import { MainComponent } from './main/main.component';
import { MenuToggleModule } from './main/menu-toggle.module';
import { MenuItems } from './core/menu/menu-items/menu-items';
import { AuthGuard } from './shared/services/auth.guard';
import { WidgetComponentModule } from './widget-component/widget-component.module';
import { SideBarComponent } from './shared/side-bar/side-bar.component';
import { FooterComponent } from './shared/footer/footer.component';
import { SharedModule } from './shared/shared.module';
import { FormlyModule } from '@ngx-formly/core';
import { OAuthModule } from 'angular-oauth2-oidc';
import { MultiTranslateHttpLoader } from './shared/services/multi-translate-loader';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
import { MonacoEditorModule } from 'ngx-monaco-editor';

// AoT requires an exported function for factories
export function translateLoader(http: HttpClient) {
	return new MultiTranslateHttpLoader(http, [
		{ prefix: './assets/i18n/', suffix: '.json' },
		{ prefix: './assets/i18n/common/common.', suffix: '.json' }
	]);
}

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
	suppressScrollX: true
};

@NgModule({
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		RoutingModule,
		FlexLayoutModule,
		TourMatMenuModule.forRoot(),
		PerfectScrollbarModule,
		MenuToggleModule,
		HttpClientModule,
		MonacoEditorModule.forRoot(),
		TranslateModule.forRoot({
			loader: {
				provide: TranslateLoader,
				useFactory: translateLoader,
				deps: [HttpClient]
			}
		}),
		ToastrModule.forRoot(),
		WidgetComponentModule,
		LoadingBarRouterModule,
		LoadingBarHttpClientModule,
		SharedModule,
		FormlyModule.forRoot(),
		OAuthModule.forRoot()
	],
	declarations: [
		XCoreAppComponent,
		MainComponent,
		SideBarComponent, FooterComponent
	],
	bootstrap: [XCoreAppComponent],
	providers: [
		MenuItems,
		PageTitleService,
		{
			provide: PERFECT_SCROLLBAR_CONFIG,
			useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
		},
		AuthGuard
	],
	schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
