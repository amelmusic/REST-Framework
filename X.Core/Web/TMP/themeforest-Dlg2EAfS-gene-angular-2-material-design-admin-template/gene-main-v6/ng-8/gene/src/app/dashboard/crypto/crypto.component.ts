import { Component, OnInit, ViewEncapsulation, ViewChild, OnDestroy } from '@angular/core';
import { CoreService } from '../../service/core/core.service';
import { MatSort, MatTableDataSource,MatPaginator} from '@angular/material';
import { Router, NavigationEnd} from '@angular/router';
import { PageTitleService } from '../../core/page-title/page-title.service';

@Component({
  selector: 'ms-crypto',
  templateUrl: './crypto.component.html',
  styleUrls: ['./crypto.component.scss']
})
export class CryptoComponent implements OnInit, OnDestroy {

   coinList               : any;
   tickerSliderContent    : any;
   statsCardData          : any;
   safeTradeContent       : any;
   exchangeStatistic      : any;
   tradeHistory           : any;
   recentTradeElement     : any;
   collapseSidebarStatus  : boolean;
   currentRoute           : any;
   @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;
   @ViewChild(MatSort,{static: false}) sort : MatSort;
   liveTradeSource = new MatTableDataSource<any>(this.coinList);

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
            "breakpoint": 1480,
            "settings": {
               "slidesToShow": 4,
               "slidesToScroll": 1
            }
         },
         {
            "breakpoint": 1280,
            "settings": {
               "slidesToShow": 3,
               "slidesToScroll": 1
            }
         },
         {
            "breakpoint": 960,
            "settings": {
               "slidesToShow": 2,
               "slidesToScroll": 1,
               "speed": 7000
            }
         },
         {
            "breakpoint": 599,
            "settings": {
               "slidesToShow": 1,
               "slidesToScroll": 1
            }
         }
      ]
   }

   //safe trade
   safeTradeConfig  = {"slidesToShow": 1,"fade": true, "slidesToScroll": 1,"arrows": false,"dots": false,"autoplay": true, "autoplaySpeed": 2000};
   
   //Exchange Statistics
   statisticConfig = {"slidesToShow": 1, "fade":true, "slidesToScroll": 1,"arrows": false,"dots": false,"autoplay": true, "autoplaySpeed": 2000};
 
   tradeHistoryColumns = ["currency","txnno", "status", "price", "total", "date","from","more"];
   
   recentTradeColumns = ["currency", "status", "price", "total"];

   cryptoCompareColumns = ["serial_number","desktop_name","mobile_name","price","volume","tag","total_volume","market_cap","circulating_supply","chart","change"];
   
   
   /*
      ----------live status Chart  ----------
   */
  
   // live status chart label
   public liveStatusChartLabel :string[] = ['9', '10', '11', '12'];
   
   //live status chart data
   public liveStatusChartData : any[] = [
      {data: [100, 200, 125, 250],label:"Live status"}
   ];

   //live status chart color
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

   constructor(public service : CoreService,
               private router : Router,
               private pageTitleService: PageTitleService) { }

   ngOnInit() {
      this.collapseSidebarStatus = this.service.collapseSidebarStatus;
      this.currentRoute = location.pathname.split('/')[1];

      this.pageTitleService.setTitle("Crypto");
      this.service.getCoinList().
         subscribe( res => {this.coinList = res},
                    err => console.log(err),
                    ()  =>  this.cryptoSelect('BTC')
         );

      this.service.getTickerData().
         subscribe( res => {this.tickerSliderContent = res},
                    err => console.log(err),
                    ()  =>  this.tickerSliderContent
         );

      this.service.getCryptoStatsCardContent().
         subscribe( res => {this.statsCardData = res},
                    err => console.log(err),
                    ()  =>  this.statsCardData
         );

      this.service.getSafeTradeContent().
         subscribe( res => {this.safeTradeContent = res},
                    err => console.log(err),
                    ()  =>  this.safeTradeContent
         );

      this.service.getExchangeStatisticsContent().
         subscribe( res => {this.exchangeStatistic = res},
                    err => console.log(err),
                    ()  =>  this.exchangeStatistic
         );

      this.service.getTradeHistoryContent().
         subscribe( res => {this.tradeHistory = res},
                    err => console.log(err),
                    ()  => this.tradeHistory
         );

      this.service.getRecentTradeContent().
         subscribe( res => {this.recentTradeElement = res},
                    err => console.log(err),
                    ()  => this.recentTradeElement
         );
   }

   //showTradeHistory method is used to open the dailog of trade history.
   showTradeHistory(element){
      this.service.showTradeHistory(element);
   }

   //cryptoSelect method is used to show the table data according to selected coin.
   cryptoSelect(data){
      this.liveTradeSource = new MatTableDataSource<any>(this.coinList);
      this.liveTradeSource.sort = this.sort;
      this.liveTradeSource.paginator = this.paginator;
      let liveTradeData = [];
      for(let content of this.coinList){
         if(content.tag ==  data){
            liveTradeData.push(content);
            this.liveTradeSource = new MatTableDataSource<any>(liveTradeData);
            this.liveTradeSource.sort = this.sort;
            this.liveTradeSource.paginator = this.paginator;
         }
      }
   }

   ngOnDestroy(){
      if(this.currentRoute != 'horizontal' && this.collapseSidebarStatus== false){
         if(document.getElementById('main-app').classList.contains('collapsed-sidebar')){
            this.service.collapseSidebar = false;
         }
      }
   }
}
