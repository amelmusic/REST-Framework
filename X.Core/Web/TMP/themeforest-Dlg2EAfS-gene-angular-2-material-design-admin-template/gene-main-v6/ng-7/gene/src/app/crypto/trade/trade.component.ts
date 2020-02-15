import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { CoreService } from '../../service/core/core.service';
import { TranslateService } from '@ngx-translate/core'

@Component({
  selector: 'ms-trade',
  templateUrl: './trade.component.html',
  styleUrls: ['./trade.component.scss']
})
export class TradeComponent implements OnInit {

   tickerSliderContent : any;
   //expenditure Chart Data 
   expenditureChartData = [{
      "title": "Product 1",
      "value": 351.9
      },{
      "title": "Product 2",
      "value": 165.8
      }, {
      "title": "Product 3",
      "value": 139.9
      }, {
      "title": "Product 4",
      "value": 128.3
   }];

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
   
   /*
      ----------live Status Chart  ----------
   */
  
   // live Status chart label
   public liveStatusChartLabel :string[] = ['9', '10', '11', '12', '13', '14', '15', '16'];
   
   //live Status chart data
   public liveStatusChartData : any[] = [
      {data: [10,26,5,30,10,26,5,30],label:"Live Status"}
   ];

   //live Status chart color
   public liveStatusChartColors: Array <any> = [
      {
         fill: false,
         lineTension: 0,
         fillOpacity: 0.3,
         pointHoverBorderWidth: 4,
         borderWidth:4,
         pointHoverRadius: 7,
         pointBorderWidth: 3,
         pointRadius: 6,
         backgroundColor: '#1565c0',
         borderColor: '#1565c0',
         pointBackgroundColor: '#1565c0',
         pointBorderColor:'#FFFFFF',
         pointHoverBackgroundColor: '#1565c0',
         pointHoverBorderColor: '#1565c0'
      }
   ];
   
   //recent trade content
   recentTradeElement : any[] = [
      {currencyIcon: "BTC-alt",statusClass: 'success-text', status: 'Buy', price: 16245.79, total: 16245.79},
      {currencyIcon: "ETC",statusClass: 'warn-text', status: 'Sell', price: 43224.06, total: 16245.79},
      {currencyIcon: "BTC-alt",statusClass: 'success-text', status: 'Buy', price: 61213.941, total: 16245.79},
      {currencyIcon: "ETC",statusClass: 'warn-text', status: 'Sell', price: 9313.0122, total: 16245.79},
      {currencyIcon: "LTC",statusClass: 'warn-text', status: 'Sell', price: 10243.11, total: 16245.79},
      {currencyIcon: "BTC-alt",statusClass: 'success-text', status: 'Buy', price: 123872.07, total: 16245.79},
      {currencyIcon: "LTC",statusClass: 'warn-text', status: 'Sell', price: 10243.11, total: 16245.79},
      {currencyIcon: "ETC",statusClass: 'warn-text', status: 'Sell', price: 43224.06, total: 16245.79},
      {currencyIcon: "BTC-alt",statusClass: 'success-text', status: 'Buy', price: 61213.941, total: 16245.79},
   ];
   displayedColumns = ['currency', 'status', 'price', 'total'];

    /*
      ---------- Buy/Sell Chart ----------
   */
  
   //line chart label
   public buySellChartLabel :string[] = ['2011', '2012', '2013', '2014', '2015','2016', '2017'];
   
   //line chart data
   public buySellChartData : any[] = [
      {data: [0, 5,  65, 7, 120, 40, 10],label:"Buy"},
      {data: [0, 15, 45, 50, 30, 80, 10],label:"Sell"},
      {data: [0, 30, 20, 40, 10, 25, 10],label:"Transfer"}
   ];

   //line chart color
   public buySellChartColors: Array <any> = [
      {
         lineTension: 0.4,
         borderColor: '#1565c0',
         pointBorderColor: '#1565c0',
         pointBorderWidth: 2,
         pointRadius: 2,
         fill: false,
         pointBackgroundColor: '#FFFFFF',
         borderWidth: 3,
      },
      {
         lineTension: 0.4,
         borderColor: '#0097a7',
         pointBorderColor: '#0097a7',
         pointBorderWidth: 2,
         pointRadius: 2,
         fill: false,
         pointBackgroundColor: '#FFFFFF',
         borderWidth: 3,
      },
      {
         lineTension: 0.4,
         borderColor: '',
         pointBorderColor: '',
         pointBorderWidth: 2,
         pointRadius: 2,
         fill: false,
         pointBackgroundColor: '#FFFFFF',
         borderWidth: 3,
      
      }
   ];

   constructor(private pageTitleService: PageTitleService,
               private service : CoreService,
               private translate : TranslateService) { }

   ngOnInit() {
      this.pageTitleService.setTitle("Trade");

      this.service.getTickerData().
         subscribe( res => {this.tickerSliderContent = res},
                    err => console.log(err),
                    ()  =>  this.tickerSliderContent
         );        
   }

}

 
   