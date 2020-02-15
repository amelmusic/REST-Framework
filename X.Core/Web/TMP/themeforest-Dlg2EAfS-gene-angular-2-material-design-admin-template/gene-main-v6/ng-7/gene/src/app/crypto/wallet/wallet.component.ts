import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { CoreService } from '../../service/core/core.service';
import { TranslateService } from '@ngx-translate/core'
import { Router } from '@angular/router';

@Component({
  selector: 'ms-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.scss']
})
export class WalletComponent implements OnInit {

	tickerSliderContent    : any;
   collapseSidebarStatus  : boolean;
   walletContent          : any;

   //ticker slider config
   tickerSliderConfig  = {
      "speed": 10000,
      "autoplay": true,
      "autoplaySpeed": 0,
      "cssEase": 'linear',
      "slidesToShow": 5, 
      "slidesToScroll": 1,
      "arrows": false,
      "dots": false,
      "responsive": [
      {
         breakpoint: 1480,
         settings: {
            slidesToShow: 4,
            slidesToScroll: 1
         }
      },
      {
         breakpoint: 1280,
         settings: {
            slidesToShow: 3,
            slidesToScroll: 1
         }
      },
      {
         breakpoint: 960,
         settings: {
            slidesToShow: 2,
            slidesToScroll: 1,
            speed: 7000
         }
      },
      {
         breakpoint: 599,
         settings: {
            slidesToShow: 1,
            slidesToScroll: 1
         }
      }
   ]};
  
	cryptoProgress : any[] = [
		{
			icon : "BTC-alt",
			name : "Bitcoin",
			trade : "30",
			progressValue : "30",
			viewers : "41",
			card_color : "primary-bg"
		},
		{
			icon : "ETC",
			name : "Ethereum",
			trade : "60",
			progressValue : "60",
			viewers : "4381",
			card_color : "success-bg"
		},
		{
			icon : "LTC-alt",
			name : "Litecoin",
			trade : "80",
			progressValue : "80",
			viewers : "2611",
			card_color : "accent-bg"
		},
		{
			icon : "ZEC-alt",
			name : "Zcash",
			trade : "40",
			progressValue : "40",
			viewers : "611",
			card_color : "warn-bg"
		}
	]

	constructor(private pageTitleService: PageTitleService,
               private service : CoreService,
               private translate : TranslateService,
               private router : Router
              ) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Wallet");

      this.service.getTickerData().
         subscribe( res => {this.tickerSliderContent = res},
                    err => console.log(err),
                    ()  =>  this.tickerSliderContent
         );   
           
      this.service.getWalletContent().
         subscribe( res => {this.walletContent = res},
                    err => console.log(err),
                    ()  =>  this.walletContent
         );     
	}

   //savedCard method is used to render the ecommerce card page.
   savedCard(){
      var first = location.pathname.split('/')[1];
      if(first == 'horizontal'){
         this.router.navigate(['/horizontal/ecommerce/cards']);
      }else {
         this.router.navigate(['/ecommerce/cards']);
      }
   }
}
