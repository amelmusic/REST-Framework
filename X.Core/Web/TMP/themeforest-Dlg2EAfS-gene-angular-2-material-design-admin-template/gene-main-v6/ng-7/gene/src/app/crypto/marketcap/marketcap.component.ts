import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { CoreService } from '../../service/core/core.service';
import { TranslateService } from '@ngx-translate/core'

@Component({
  selector: 'ms-marketcap',
  templateUrl: './marketcap.component.html',
  styleUrls: ['./marketcap.component.scss']
})
export class MarketcapComponent implements OnInit {
   
   marketCap           : any ;
   tickerSliderContent : any ;
   changeType          : any = 'year';
   //stat progress card
   statsProgressCard : any = [
      {
         icon : "BTC-alt",
         coinName : "Bitcoin",
         trade : "30%",
         progress : "30",
         code : "+ 41",
         bg_color : "primary-bg"
      },
      {
         icon : "ETC",
         coinName : "Ethereum",
         trade : "60%",
         progress : "60",
         code : "+ 4381",
         bg_color : "success-bg"
      },
      {
         icon : "LTC-alt",
         coinName : "Litecoin",
         trade : "80%",
         progress : "80",
         code : "+ 2611",
         bg_color : "accent-bg"
      },
      {
         icon : "ZEC-alt",
         coinName : "Zcash",
         trade : "40%",
         progress : "40",
         code : "+ 611",
         bg_color : "warn-bg"
      }
   ]

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

	constructor(private pageTitleService: PageTitleService,
               private service : CoreService,
               private translate : TranslateService) { }

	ngOnInit() {

		this.pageTitleService.setTitle("Market Cap");

      this.service.getMarketCap().
         subscribe( res => {this.marketCap = res},
                    err => console.log(err),
                    ()  =>  this.marketCap
         );

      this.service.getTickerData().
         subscribe( res => {this.tickerSliderContent = res},
                    err => console.log(err),
                    ()  =>  this.tickerSliderContent
         );
	}

   //dataChange method is used to change the chart data according to button event.
   dataChange(data){
      this.changeType = data;
   }
}
