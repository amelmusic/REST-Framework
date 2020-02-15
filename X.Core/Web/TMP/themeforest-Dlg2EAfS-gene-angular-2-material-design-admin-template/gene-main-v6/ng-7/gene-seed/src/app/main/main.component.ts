import {filter} from 'rxjs/operators';
import { Component, OnInit, OnDestroy, ViewChild, HostListener, ViewEncapsulation} from '@angular/core';
import { MenuItems } from '../core/menu/menu-items/menu-items';
import { BreadcrumbService } from 'ng5-breadcrumb';
import { PageTitleService } from '../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';
import { Router, NavigationEnd,ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { MediaChange, MediaObserver} from "@angular/flex-layout";
import { DeviceDetectorService } from 'ngx-device-detector';
import { TourService } from 'ngx-tour-md-menu';
import PerfectScrollbar from 'perfect-scrollbar';
import { AuthService } from '../service/auth-service/auth.service';
import { CoreService } from '../service/core/core.service';

const screenfull = require('screenfull');

@Component({
   selector: 'gene-layout',
   templateUrl:'./main-material.html',
   styleUrls: ['./main-material.scss'],
   encapsulation: ViewEncapsulation.None
})

export class MainComponent implements OnInit, OnDestroy{

   currentUrl            : any;
   root                  : any = 'ltr';
   layout                : any = 'ltr';
   currentLang           : any = 'en';
   customizerIn          : boolean = false;
   showSettings          : boolean = false;
   chatpanelOpen         : boolean = false;
   sidenavOpen           : boolean = true;
   isMobile              : boolean = false;   
   isFullscreen          : boolean = false;
   header                : string;
   url                   : string;
   dark                  : boolean;
   compactSidebar        : boolean;
   isMobileStatus        : boolean;
   sidenavMode           : string = 'side';
   popupDeleteResponse   : any;
   sidebarColor          : any;
   isSidebarFilterClass  : string;
   isHeaderFilterClass   : string;
   deviceInfo = null;
   private _mediaSubscription         : Subscription;
   private _routerEventsSubscription  : Subscription;
   private _router                    : Subscription;
   @ViewChild('sidenav') sidenav;

   sideBarFilterClass : any = [
      {
         sideBarSelect  :"sidebar-color-1",
         colorSelect    :"sidebar-color-dark"
      },
      {
         sideBarSelect  :"sidebar-color-2",
         colorSelect    :"sidebar-color-primary",
      },
      {
         sideBarSelect  :"sidebar-color-3",
         colorSelect    :"sidebar-color-accent"
      },
      {
         sideBarSelect  :"sidebar-color-4",
         colorSelect    :"sidebar-color-warn"
      },
      {
         sideBarSelect  :"sidebar-color-5",
         colorSelect    :"sidebar-color-green"
      }
   ]

   headerFilterClass : any = [
      {
         headerSelect  :"header-color-1",
         colorSelect   :"header-color-dark"
      },
      {
         headerSelect  :"header-color-2",
         colorSelect   :"header-color-primary"
      },
      {
         headerSelect  :"header-color-3",
         colorSelect   :"header-color-accent"
      },
      {
         headerSelect  :"header-color-4",
         colorSelect   :"header-color-warning"
      },
      {
         headerSelect  :"header-color-5",
         colorSelect   :"header-color-green"
      }
   ]

   chatList : any [] = [
      {
         image : "assets/img/user-1.jpg",
         name: "John Smith",
         chat : "Lorem ipsum simply dummy",
         mode : "online"
      },
      {
         image : "assets/img/user-2.jpg",
         name: "Amanda Brown",
         chat : "Lorem ipsum simply dummy",
         mode : "online"
      },
      {
         image : "assets/img/user-3.jpg",
         name: "Justin Randolf",
         chat : "Lorem ipsum simply dummy",
         mode : "offline"
      },
      {
         image : "assets/img/user-4.jpg",
         name: "Randy SunSung",
         chat : "Lorem ipsum simply dummy",
         mode : "online"
      },
      {
         image : "assets/img/user-5.jpg",
         name: "Lisa Myth",
         chat : "Lorem ipsum simply dummy",
         mode : "online"
      },
   ]

   constructor(public tourService: TourService, 
               public menuItems: MenuItems, 
               private breadcrumbService: BreadcrumbService, 
               private pageTitleService: PageTitleService, 
               public translate: TranslateService, 
               private router: Router,
               private media: MediaObserver,
               private deviceService: DeviceDetectorService,
               private authService : AuthService,
               public coreService : CoreService,
               private routes :Router,
               private activatedRoute: ActivatedRoute ) {   

      const browserLang: string = translate.getBrowserLang();
      translate.use(browserLang.match(/en|fr/) ? browserLang : 'en');

      this.tourService.initialize([{
         anchorId: 'start.tour',
         content: 'Welcome to Gene admin panel!',
         placement: 'below',
         title: 'Welcome to Gene',
      },
      {
         anchorId: 'tour-search',
         content: 'Enjoying Search box with sugestion and many more things',
         placement: 'below',
         title: 'Search Box',
      },
      {
         anchorId: 'tour-full-screen',
         content: 'By pressing this button you can switch to fullscreen mode.',
         placement: 'below',
         title: 'Full Screen',
      },
      {
         anchorId: 'tour-ui',
         content: 'Show your site stats with unique designed cards',
         placement: 'below',
         title: 'Stats Cards',
      }]);

      if(window.innerWidth>959){
         this.tourService.start();
      }

      breadcrumbService.addFriendlyNameForRoute('/dashboard', 'Dashboard');
      breadcrumbService.addFriendlyNameForRoute('/dashboard/dashboard-v1', 'Dashboard V1');
   }

   ngOnInit() {
      this.pageTitleService.title.subscribe((val: string) => {
         this.header = val;
      });

      setTimeout(()=>{
         if((this.router.url === '/dashboard/courses' || this.router.url === '/courses/courses-list' || this.router.url === '/courses/course-detail') && window.innerWidth<1920){
            this.coreService.sidenavMode = 'over';
            this.coreService.sidenavOpen = false;
            document.getElementById('main-app').className += " sidebar-overlay";
         }
         else if ((this.router.url === '/ecommerce/shop' || this.router.url === '/ecommerce/checkout' || this.router.url === '/ecommerce/invoice') && window.innerWidth<1920){
            this.coreService.sidenavMode = 'over';
            this.coreService.sidenavOpen = false;
            //for responsive
            document.getElementById('main-app').className += " sidebar-overlay";
         }
         else if(window.innerWidth>1280){
            this.coreService.sidenavMode = 'side';
            this.coreService.sidenavOpen = true;
            //for responsive
            var main_div = document.getElementsByClassName('app');
            for(let i = 0; i<main_div.length; i++){
               if(main_div[i].classList.contains('sidebar-overlay')){
                  document.getElementById('main-app').classList.remove('sidebar-overlay');
               }
            }
         }
         //for responsive
         else if(window.innerWidth<1280){
            var main_div = document.getElementsByClassName('app');
            for(let i = 0; i<main_div.length; i++){
               if(!(main_div[i].classList.contains('sidebar-overlay'))){
                  document.getElementById('main-app').className += " sidebar-overlay";
               }
            }
         }
      },0)
    
      this._router = this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe((event: NavigationEnd) => {
         this.url = event.url;
      });

      if (this.url != '/session/login' && this.url != '/session/register' && this.url != '/session/forgot-password' && this.url != '/session/lockscreen') {
         const elemSidebar = <HTMLElement>document.querySelector('.sidebar-container ')
      }
    
      this.deviceInfo = this.deviceService.getDeviceInfo();
      if(this.deviceInfo.device == 'ipad' || this.deviceInfo.device == 'iphone' || this.deviceInfo.device == 'android' ){
         this.coreService.sidenavMode = 'over';
         this.coreService.sidenavOpen = false;
         document.getElementById('main-app').className += " sidebar-overlay";
      }

      this._mediaSubscription = this.media.media$.subscribe((change: MediaChange) => {
         if(this.coreService.horizontalStatus){
            if(window.innerWidth<=959){
               this.coreService.horizontalSizeStatue = false; 
            }
            else{
               this.coreService.horizontalSizeStatue = true;
            }
         }
         this.isMobileStatus = (change.mqAlias == 'xs') || (change.mqAlias == 'sm') || (change.mqAlias == 'md');
         this.isMobile = this.isMobileStatus;
         if(this.isMobile || ((this.router.url === '/ecommerce/shop' || this.router.url === '/ecommerce/checkout' || this.router.url === '/ecommerce/invoice' || this.router.url === '/dashboard/courses' || this.router.url === '/courses/courses-list' || this.router.url === '/courses/course-detail') && window.innerWidth<1920)) {
            this.coreService.sidenavMode = 'over';  
            this.coreService.sidenavOpen = false;
            //for responsive
            var main_div = document.getElementsByClassName('app');
            for(let i = 0; i<main_div.length; i++){
               if(!(main_div[i].classList.contains('sidebar-overlay'))){
                  if(document.getElementById('main-app')) {
                     document.getElementById('main-app').className += " sidebar-overlay";
                  }
               }
            }
         }
         else {
            this.coreService.sidenavMode = 'side';
            this.coreService.sidenavOpen = true;
            //for responsive
            var main_div = document.getElementsByClassName('app');
            for(let i = 0; i<main_div.length; i++){
               if(main_div[i].classList.contains('sidebar-overlay')){
                  document.getElementById('main-app').classList.remove('sidebar-overlay');
               }
            }
         }
      });

      this._routerEventsSubscription = this.router.events.subscribe((event) => {
         if (event instanceof NavigationEnd && this.isMobile) {
            this.sidenav.close();
         }
      });
   }

   ngOnDestroy() {
      this._router.unsubscribe();
      this._mediaSubscription.unsubscribe();
   }
    
   collapseSidebarStatus : boolean;
   /**
     *As router outlet will emit an activate event any time a new component is being instantiated.
     */
   onActivate(e, scrollContainer) {
      scrollContainer.scrollTop = 0;
      if((this.router.url === '/dashboard/courses' || this.router.url === '/courses/courses-list' || this.router.url === '/courses/course-detail') && window.innerWidth<1920){
         this.coreService.sidenavMode = 'over';
         this.coreService.sidenavOpen = false;
         document.getElementById('main-app').className += " sidebar-overlay";
      }
      else if ((this.router.url === '/ecommerce/shop' || this.router.url === '/ecommerce/checkout' || this.router.url === '/ecommerce/invoice') && window.innerWidth<1920){
         this.coreService.sidenavMode = 'over';
         this.coreService.sidenavOpen = false;
         //for responsive
         document.getElementById('main-app').className += " sidebar-overlay";
      }
      else if (window.innerWidth>1280){
         this.coreService.sidenavMode = 'side';
         this.coreService.sidenavOpen = true;
         //for responsive
         var main_div = document.getElementsByClassName('app');
         for(let i = 0; i<main_div.length; i++){
            if(main_div[i].classList.contains('sidebar-overlay')){
               document.getElementById('main-app').classList.remove('sidebar-overlay');
            }
         }
      }
      
      if(this.router.url == '/dashboard/crypto' || this.router.url == '/crypto/marketcap' || this.router.url == '/crypto/wallet' || this.router.url == '/crypto/trade') {
         this.collapseSidebarStatus = this.coreService.collapseSidebar;
         if((this.collapseSidebarStatus == false)  && (window.innerWidth>1280)) {
            document.getElementById('main-app').className += ' collapsed-sidebar';
            this.coreService.collapseSidebar = true; 
         }
      }
      else if((this.collapseSidebarStatus == false)  && (window.innerWidth>1280)) {
         document.getElementById('main-app').classList.remove('collapsed-sidebar');
         this.coreService.collapseSidebar = false;
      }
   }

   /**
     * toggleFullscreen method is used to show a template in fullscreen.
     */
	toggleFullscreen() {
    	if (screenfull.enabled) {
    		screenfull.toggle();
      		this.isFullscreen = !this.isFullscreen;
    	}
  	}
   
   /**
     * customizerFunction is used to open and close the customizer.
     */
   customizerFunction() {
      this.customizerIn = !this.customizerIn;
   }

   /**
     * addClassOnBody method is used to add a add or remove class on body.
     */
   addClassOnBody(event) {
      var body = document.body;
      if(event.checked){
         body.classList.add("dark-theme-active");
      }else{
         body.classList.remove('dark-theme-active');
      }
   }

   /**
     * addMenuItem is used to add a new menu into menu list.
     */
   addMenuItem(): void {
      this.menuItems.add({
         state: 'pages',
         name: 'GENE MENU',
         type: 'sub',
         icon: 'trending_flat',
         children: [
            {state: 'blank', name: 'SUB MENU1'},
            {state: 'blank', name: 'SUB MENU2'}
         ]
      });
   }

   /**
     * changeRTL method is used to change the layout of template.
     */
   changeRTL(isChecked) {
      if(isChecked){
         this.layout = "rtl"
      }  
      else {
         this.layout = "ltr"
      }
   }

   /**
     * toggleSidebar method is used a toggle a side nav bar.
     */
   toggleSidebar() {
      this.coreService.sidenavOpen = !this.coreService.sidenavOpen;
   }

   /**
     * logOut method is used to log out the  template.
     */
   logOut() {    
      this.authService.logOut();
   }

   /**
     * sidebarFilter function filter the color for header section.
     */
   sidebarFilter(selectedFilter){
      document.getElementById('main-app').classList.remove('sidebar-color-dark');
      this.isSidebarFilterClass = selectedFilter.colorSelect;
      document.querySelector('.radius-circle').classList.remove('radius-circle');
      document.getElementById(selectedFilter.sideBarSelect).classList.add('radius-circle');
   }

   /**
     * sidebarFilter function filter the color for header section.
     */
   headerFilter(selectedFilter){
      document.getElementById('main-app').classList.remove('header-color-dark');
      this.isHeaderFilterClass = selectedFilter.colorSelect;
      document.querySelector('.radius-active').classList.remove('radius-active');
      document.getElementById(selectedFilter.headerSelect).classList.add('radius-active');
   }

   /**
     *chatMenu method is used to toggle a chat menu list.
     */
   chatMenu() {
      document.getElementById("gene-chat").classList.toggle("show-chat-list");
   }

   /**
     * onChatOpen method is used to open a chat window.
     */
   onChatOpen() {
      document.getElementById('chat-open').classList.toggle('show-chat-window');
   }

   /**
     * onChatWindowClose method is used to close the chat window.
     */  
   chatWindowClose(){
      document.getElementById("chat-open").classList.remove("show-chat-window");
   }

   /**
     * changeLayout method is used to change the vertical layout to horizontal layout.
     */
   changeLayout() {
      this.coreService.horizontalStatus = true;
      if(window.innerWidth<=959){
         this.coreService.horizontalSizeStatue = false;
      }
      else{
         this.coreService.horizontalSizeStatue = true;
      }
   }
}


