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
import { ChartsModule } from 'ng2-charts';
import { QuillModule } from 'ngx-quill';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule } from '@ngx-translate/core';
import { EmbedVideo } from 'ngx-embed-video';
import { BarRatingModule } from "ngx-bar-rating";
import { RouterModule } from '@angular/router';

import { SlickCarouselModule } from 'ngx-slick-carousel';
import { EasyPieChartModule } from 'ng2modules-easypiechart';
import { nvD3 } from "../core/nvD3/nvD3.component";
import { ShopGridComponent } from './shop-type/shop-grid/shop-grid.component';
import { DeleteDialogComponent } from './pop-up/delete-dialog/delete-dialog.component';
import { LineChartComponent } from './chart/line-chart/line-chart.component';
import { StackedAreaChartComponent } from './chart/stacked-area-chart/stacked-area-chart.component';
import { PieChartComponent } from './chart/pie-chart/pie-chart.component';
import { AddNewCardComponent } from './pop-up/add-new-card/add-new-card.component';
import { InboxComposeComponent } from './pop-up/inbox-compose/inbox-compose.component';
import { AddNewUserComponent } from './pop-up/add-new-user/add-new-user.component';
import { EditNewUserComponent } from './pop-up/edit-new-user/edit-new-user.component';
import { LanguageDropDownComponent } from './global/language-drop-down/language-drop-down.component';
import { UserProfileComponent } from './user-profileV2/user-profile/user-profile.component';
import { ResearchInterestsComponent } from './user-profileV2/research-interests/research-interests.component';
import { FollowersComponent } from './user-profileV2/followers/followers.component';
import { PublicationsComponent } from './user-profileV2/publications/publications.component';
import { VideoPlayerComponent } from './pop-up/video-player/video-player.component';
import { ShopListComponent } from './shop-type/shop-list/shop-list.component';
import { PaymentMessageComponent } from './pop-up/payment-message/payment-message.component';
import { StatsCardComponent } from './stats-card/stats-card.component';
import { StatsLineChartComponent } from './chart/stats-line-chart/stats-line-chart.component';
import { CandlestickChartComponent } from './chart/candlestick-chart/candlestick-chart.component';
import { SafeTradeSliderComponent } from './slider/safe-trade-slider/safe-trade-slider.component';
import { TradeDialogComponent } from './pop-up/trade-dialog/trade-dialog.component';
import { ExchangeStatisticsComponent } from './slider/exchange-statistics/exchange-statistics.component';
import { Ng2PieChartComponent } from './chart/ng2-pie-chart/ng2-pie-chart.component';
import { ColumnChartWithImagesComponent } from './chart/column-chart-with-images/column-chart-with-images.component';
import { AmchartsPieChartComponent } from './chart/amcharts-pie-chart/amcharts-pie-chart.component';
import { AddNewClientComponent } from './pop-up/add-new-client/add-new-client.component';
import { EditNewClientComponent } from './pop-up/edit-new-client/edit-new-client.component';
import { StatisticsComponent } from './chart/statistics-chart/statistics-chart.component';
import { BuySellChartComponent } from './chart/buy-sell-chart/buy-sell-chart.component';
import { MarketCapChartsComponent } from './chart/market-cap-charts/market-cap-charts.component';
import { TickerSliderComponent } from './slider/ticker-slider/ticker-slider.component';
import { SocialCardComponent } from './social-share/social-card/social-card.component';
import { SocialCardV2Component } from './social-share/social-card-v2/social-card-v2.component';
import { SaleCardComponent } from './card/sale-card/sale-card.component';
import { ServerCardComponent } from './card/server-card/server-card.component';
import { CoursesCardComponent } from './courses-widget/courses-card/courses-card.component';
import { InstructorCardComponent } from './courses-widget/instructor-card/instructor-card.component';
import { CoursesBannerComponent } from './courses-widget/courses-banner/courses-banner.component';
import { CoursesDescriptionComponent } from './courses-widget/courses-description/courses-description.component';
import { CourseDetailBannerComponent } from './courses-widget/course-detail-banner/course-detail-banner.component';
import { CourseDetailDescriptionComponent } from './courses-widget/course-detail-description/course-detail-description.component';
import { CourseDetailInstructorComponent } from './courses-widget/course-detail-instructor/course-detail-instructor.component';
import { CourseDetailBillingComponent } from './courses-widget/course-detail-billing/course-detail-billing.component';
import { CourseDetailOverviewComponent } from './courses-widget/course-detail-overview/course-detail-overview.component';
import { CourseDetailLearnComponent } from './courses-widget/course-detail-learn/course-detail-learn.component';
import { FullWidthGraphComponent } from './chart/full-width-graph/full-width-graph.component';
import { YearlySaleComponent } from './chart/yearly-sale/yearly-sale.component';
import { SalesReportComponent } from './chart/sales-report/sales-report.component';
import { EmailStatisticsComponent } from './chart/email-statistics/email-statistics.component';
import { BrowserStackComponent } from './chart/browser-stack/browser-stack.component';
import { LiveChartSupportComponent } from './chart/live-chart-support/live-chart-support.component';
import { MixedChartComponent } from './chart/mixed-chart/mixed-chart.component';
import { SocialShareIconsComponent } from './social-share/social-share-icons/social-share-icons.component';
import { ImgZoomComponent } from './img-zoom/img-zoom.component';
import { SaasDashboardCardComponent } from './card/saas-dashboard-card/saas-dashboard-card.component';

@NgModule({
	declarations: [
		ShopGridComponent,
		DeleteDialogComponent,
		AddNewCardComponent,
		LineChartComponent,
		StackedAreaChartComponent,
		PieChartComponent,
		nvD3,
		InboxComposeComponent,
		AddNewUserComponent,
		EditNewUserComponent,
		LanguageDropDownComponent,
		UserProfileComponent,
		ResearchInterestsComponent,
		FollowersComponent,
		PublicationsComponent,
		VideoPlayerComponent,
		ShopListComponent,
		PaymentMessageComponent,
		StatsCardComponent,
		StatsLineChartComponent,
		CandlestickChartComponent,
		SafeTradeSliderComponent,
		TradeDialogComponent,
		ExchangeStatisticsComponent,
		Ng2PieChartComponent,
		ColumnChartWithImagesComponent,
		AmchartsPieChartComponent,
		AddNewClientComponent,
		EditNewClientComponent,
		StatisticsComponent,
		BuySellChartComponent,
		MarketCapChartsComponent,
		TickerSliderComponent,
		SocialCardComponent,
		SocialCardV2Component,
		SaleCardComponent,
		ServerCardComponent,
		CoursesCardComponent,
		InstructorCardComponent, 
		CoursesBannerComponent, 
		CoursesDescriptionComponent, 
		CourseDetailBannerComponent, 
		CourseDetailDescriptionComponent, 
		CourseDetailInstructorComponent, 
		CourseDetailBillingComponent, 
		CourseDetailOverviewComponent, 
		CourseDetailLearnComponent, 
		FullWidthGraphComponent, 
		YearlySaleComponent,
		SalesReportComponent, 
		EmailStatisticsComponent, 
		BrowserStackComponent, 
		LiveChartSupportComponent, 
		MixedChartComponent,
		SocialShareIconsComponent,
		ImgZoomComponent,
		SaasDashboardCardComponent
	],
	imports: [
		RouterModule,
		BarRatingModule,
		EasyPieChartModule,
		CommonModule,
		MatCardModule,
		FlexLayoutModule,
		MatInputModule,
		MatButtonModule,
		MatIconModule,
		QuillModule,
		MatExpansionModule,
		MatDialogModule,
		MatFormFieldModule,
		MatSelectModule,
		MatMenuModule,
		MatDividerModule,
		FormsModule,
		ReactiveFormsModule,
		TextMaskModule,
		ChartsModule,
		MatSnackBarModule,
		SlickCarouselModule,
		TranslateModule,
		MatChipsModule,
		MatListModule,
		EmbedVideo.forRoot()

	],
	exports: [
		ShopGridComponent,
		LineChartComponent,
		PieChartComponent,
		StackedAreaChartComponent,
		LanguageDropDownComponent,
		FollowersComponent,
		ResearchInterestsComponent,
		UserProfileComponent,
		PublicationsComponent,
		ShopListComponent,
		StatsCardComponent,
		StatsLineChartComponent,
		CandlestickChartComponent,
		SafeTradeSliderComponent,
		ExchangeStatisticsComponent,
		Ng2PieChartComponent,
		ColumnChartWithImagesComponent,
		AmchartsPieChartComponent,
		StatisticsComponent,
		BuySellChartComponent,
		MarketCapChartsComponent,
		TickerSliderComponent,
		SocialCardComponent,
		SocialCardV2Component,
		SaleCardComponent,
		ServerCardComponent,
		CoursesCardComponent,
		InstructorCardComponent, 
		CoursesBannerComponent, 
		CoursesDescriptionComponent, 
		CourseDetailBannerComponent, 
		CourseDetailDescriptionComponent, 
		CourseDetailInstructorComponent, 
		CourseDetailBillingComponent, 
		CourseDetailOverviewComponent, 
		CourseDetailLearnComponent,
		FullWidthGraphComponent,
		YearlySaleComponent,
		SalesReportComponent,
		EmailStatisticsComponent,
		LiveChartSupportComponent,
		MixedChartComponent,
		BrowserStackComponent,
		SocialShareIconsComponent,
		ImgZoomComponent,
		SaasDashboardCardComponent
	],
	entryComponents : [
		DeleteDialogComponent,
		AddNewCardComponent,
		InboxComposeComponent,
		AddNewUserComponent,
		EditNewUserComponent,
		VideoPlayerComponent,
		PaymentMessageComponent,
		TradeDialogComponent,
		AddNewClientComponent,
		EditNewClientComponent
	]
})

export class WidgetComponentModule { }
