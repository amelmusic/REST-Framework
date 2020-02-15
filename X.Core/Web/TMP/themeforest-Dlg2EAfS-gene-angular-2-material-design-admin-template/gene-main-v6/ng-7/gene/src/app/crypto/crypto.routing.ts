import { Routes } from '@angular/router';
import { MarketcapComponent } from './marketcap/marketcap.component';
import { WalletComponent } from './wallet/wallet.component';
import { TradeComponent } from './trade/trade.component';

export const cryptoRouters : Routes = [
	{
		path : '',
		redirectTo : 'marketcap',
		pathMatch : 'full'
	},
	{
		path : '',
		children : [
			{
				path: "marketcap",
				component : MarketcapComponent
			},
			{
				path: "wallet",
				component : WalletComponent
			},
			{
				path: "trade",
				component : TradeComponent
			}
		]
	}	
]