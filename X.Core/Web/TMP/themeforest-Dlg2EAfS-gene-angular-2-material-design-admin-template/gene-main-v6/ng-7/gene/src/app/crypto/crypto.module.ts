import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'; 
import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { MatIconModule,
			MatButtonModule,
			MatSortModule,
			MatTabsModule,
			MatCardModule,
			MatMenuModule,
         MatCheckboxModule,         
			MatTableModule,
			MatDividerModule,
			MatProgressBarModule,
         MatInputModule,      
         MatOptionModule,      
         MatSelectModule,    
			MatExpansionModule,  
			MatFormFieldModule,
			MatListModule,
			MatPaginatorModule} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ChartsModule } from 'ng2-charts';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { WidgetComponentModule } from '../widget-component/widget-component.module';
import { cryptoRouters } from './crypto.routing';
import { MarketcapComponent } from './marketcap/marketcap.component';
import { WalletComponent } from './wallet/wallet.component';
import { TradeComponent } from './trade/trade.component';

@NgModule({
	declarations: [MarketcapComponent, WalletComponent, TradeComponent],
	imports: [
		CommonModule,
      RouterModule.forChild(cryptoRouters),
      MatIconModule,
		MatButtonModule,
		MatTabsModule,
      MatCardModule,      
		MatTableModule,
		MatMenuModule,
		MatListModule,
		ChartsModule,
		MatSortModule,
		MatCheckboxModule,
      MatDividerModule,      
		MatProgressBarModule,
		MatInputModule,
      MatFormFieldModule,
      PerfectScrollbarModule,
      MatExpansionModule,
		NgxDatatableModule,
      FlexLayoutModule,         
      MatOptionModule,
      MatSelectModule,
      WidgetComponentModule,
      SlickCarouselModule,
      MatPaginatorModule,
      TranslateModule
	]
})
export class CryptoModule { }
