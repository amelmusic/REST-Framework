import { Injectable } from '@angular/core';

export interface ChildrenItems {
  state: string;
  name: string;
  type?: string;
}

export interface Menu {
  state: string;
  name: string;
  type: string;
  icon: string;
  children?: ChildrenItems[];
}

const MENUITEMS = [
   {
      state: 'vertical',
      name: 'Vertical Menu',
      type: 'button',
      icon: ''
   },
   {
      name : 'General',
      type : 'sub',
      class: 'group-title',
      icon : '',
      children: [
         {
            state: 'horizontal/dashboard',
            name: 'DASHBOARD',
            type: 'subChild',
            icon: 'explore',
            children: [
               {state: 'crm', name: 'CRM',type:'link'},
               {state: 'crypto', name: 'CRYPTO',type:'link'},
               {state: 'courses', name: 'COURSES',type:'link'},
               {state: 'saas', name: 'SAAS',type:'link'},
               {state: 'web-analytics', name: 'WEB ANALYTICS',type:'link'},
            ]
         },
         {
            state: 'horizontal/crypto',
            name: 'CRYPTO',
            type: 'subChild',
            icon: 'account_balance_wallet',
            label:'New',
            children: [
               {state: 'marketcap', name: 'MARKET CAP',type:'link'},
               {state: 'wallet', name: 'WALLET',type:'link'},
               {state: 'trade', name: 'TRADE',type:'link'}
            ]
         },
         {
            state: 'horizontal/crm',
            name: 'CRM',
            type: 'subChild',
            icon: 'supervised_user_circle',
            label:'New',
            children: [
               {state: 'projects', name: 'PROJECTS',type:'link'},
               {state: '/project-detail/01', name: 'PROJECT DETAIL',type:'link'},
               {state: 'clients', name: 'CLIENTS',type:'link'},
               {state: 'reports', name: 'REPORTS',type:'link'}
            ]
         },
         {
            state: 'horizontal/courses',
            name: 'COURSES',
            type: 'subChild',
            icon: 'book',
            children: [
               {state: 'courses-list', name: 'COURSES LIST',type:'link'},
               {state: 'course-detail', name: 'COURSE DETAIL',type:'link'},
               {state: 'signin', name: 'SIGN IN',type:'link'},
               {state: 'payment', name: 'PAYMENT',type:'link'} 
            ]
         },
         {
            state: 'horizontal/ecommerce',
            name: 'E-COMMERCE',
            type: 'subChild',
            icon: 'explore',
            children: [
               {state: 'shop', name: 'SHOP WITH ALGOLIA',type:'link'},
               {state: 'products', name: 'SHOP', type:'link'},
               {state: 'edit-products', name: 'EDIT PRODUCTS', type:'link'},
               {state: 'product-add', name: 'ADD PRODUCT',type:'link'},
               {state: 'cart', name: 'CART',type:'link'},
               {state: 'checkout', name: 'CHECKOUT',type:'link'},
               {state: 'cards', name: 'CARDS',type:'link'},
               {state: 'invoice', name: 'INVOICE',type:'link'}    
            ]
         },
         {
            state: 'horizontal/inbox',
            name: 'INBOX',
            type: 'link',
            icon: 'mail'
         },
         {
            state: 'horizontal/chat',
            name: 'CHAT',
            type: 'link',
            icon: 'chat'
         },
         {
            state: 'horizontal/calendar',
            name: 'CALENDAR',
            type: 'link',
            icon: 'date_range'
         }
      ]
   },
   {
      name : 'Components',
      type : 'sub',
      class: 'group-title',
      icon : '',
      children: [
         {
            state: 'horizontal/editor',
            name: 'EDITOR',
            type: 'subChild',
            icon: 'format_shapes',
            children: [
               {   state: 'wysiwyg', type: 'link', name: 'WYSIWYG EDITOR'},
               {   state: 'ckeditor', type: 'link', name: 'CKEDITOR'}
            ]
         },
         {
            state: 'horizontal/icons',
            name: 'MATERIAL ICONS',
            type: 'link',
            icon: 'grade'
         },
         {
            state: 'horizontal/chart',
            name: 'CHARTS',
            type: 'subChild',
            icon: 'show_chart',
            children: [
               {state: 'ng2-charts', type: 'link', name: 'NG2 CHARTS'},
               {state: 'easy-pie-chart', type: 'link', name: 'EASY PIE'}
            ]
         },
         {
            state: 'horizontal/taskboard',
            name: 'TASK BOARD',
            type: 'link',
            icon: 'drag_indicator'
         }
      ]
   },
   {
      name : 'UI Elements',
      type : 'sub',
      class: 'group-title',
      mega : true,
      showColumns : 'show-column-4',
      icon : '',
      children: [
         {
            state: 'horizontal/components',
            name: 'UI COMPONENTS',
            type: 'subChild',
            icon: 'layers',
            children: [
               {state: 'cards',type: 'link', name: 'CARDS'},
               {state: 'grid', type: 'link',name: 'GRID'},
               {state: 'list',type: 'link', name: 'LIST'},
               {state: 'menu', type: 'link',name: 'MENU'},
               {state: 'slider',type: 'link', name: 'SLIDER'},
               {state: 'snackbar',type: 'link', name: 'SNACKBAR'},
               {state: 'tooltip', type: 'link',name: 'TOOLTIP'},
               {state: 'dialog', type: 'link',name: 'DIALOG'},    
               {state: 'toolbar', type: 'link',name: 'TOOLBAR'},
               {state: 'progress', type: 'link',name: 'PROGRESS'},
               {state: 'tabs',type: 'link', name: 'TABS'},
               {state: 'colorpicker',type: 'link', name: 'COLORPICKER'},
               {state: 'datepicker',type: 'link', name: 'DATEPICKER'}
            ]
         },
         {
            state: 'horizontal/components',
            name: 'FORM COMPONENTS',
            type: 'subChild',
            icon: 'layers',
            children: [
               {state: 'buttons',type: 'link', name: 'BUTTONS'},
               {state: 'select',type: 'link', name: 'SELECT'},
               {state: 'input', type: 'link',name: 'INPUT'},
               {state: 'checkbox',type: 'link', name: 'CHECKBOX'},
               {state: 'radio', type: 'link',name: 'RADIO'}
            ]
         },
         {
            state: 'horizontal/dragndrop',
            name: 'DRAG & DROP',
            type: 'subChild',
            icon: 'mouse',
            children: [
               {state: 'dragula',type: 'link', name: 'DRAGULA'},
               {state: 'sortable',type: 'link', name: 'SORTABLEJS'}
            ]
         },
          {
            state: 'horizontal/tables',
            name: 'TABLES',
            type: 'subChild',
            icon: 'format_line_spacing',
            children: [
               {state: 'fullscreen',type: 'link',name: 'FULLSCREEN'},
               {state: 'selection', type: 'link',name: 'SELECTION'},
               {state: 'pinning',type: 'link', name: 'PINNING'},
               {state: 'sorting',type: 'link', name: 'SORTING'},
               {state: 'paging',type: 'link', name: 'PAGING'},
               {state: 'editing',type: 'link', name: 'EDITING'},
               {state: 'filter',type: 'link', name: 'FILTER'},
               {state: 'responsive',type: 'link', name: 'RESPONSIVE'}
            ]
         },
         {
            state: 'horizontal/chart',
            name: 'CHARTS',
            type: 'subChild',
            icon: 'show_chart',
            children: [
               {state: 'ng2-charts', type: 'link', name: 'NG2 CHARTS'},
               {state: 'easy-pie-chart', type: 'link', name: 'EASY PIE'},
            ]
         },
         {
            state: 'horizontal/forms',
            name: 'FORMS',
            type: 'subChild',
            icon: 'insert_comment',
            children: [
               {state: 'form-wizard',type: 'link', name: 'FORM WIZARD'},
               {state: 'form-validation',type: 'link', name: 'FORM VALIDATION'},
               {state: 'form-upload',type: 'link', name: 'UPLOAD'},
               {state: 'form-tree', type: 'link',name: 'TREE'}
            ]
         },
         {
            state: 'horizontal/maps',
            name: 'MAPS',
            type: 'subChild',
            icon: 'map',
            children: [
               {state: 'googlemap', type: 'link',name: 'GOOGLE MAP'},
               {state: 'leafletmap', type: 'link',name: 'LEAFLET MAP'}
            ]
         },
         {
            state: 'horizontal/video-player',
            name: 'VIDEO PLAYER',
            type: 'link',
            icon: 'videocam'
         },
      ]
   },
   {
      name : 'Pages',
      type : 'sub',
      class: 'group-title',
      mega : true,
      showColumns : 'show-column-3',
      icon : '',
      children: [
         {
            state: 'horizontal/pages',
            name: 'PAGES',
            type: 'subChild',
            icon: 'import_contacts',
            children: [
               {state: 'media', name: 'GALLERY',type: 'link'},
               {state: 'mediaV2', name: 'GALLERY V2',type: 'link'},
               {state: 'pricing', name: 'PRICING',type: 'link'},
               {state: 'pricing-1', name: 'PRICING V2',type: 'link'},
               {state: 'blank', name: 'BLANK',type: 'link'},
               {state: 'timeline', name: 'TIMELINE',type: 'link'},
               {state: 'faq', name: 'FAQ',type: 'link'},
               {state: 'feedback', name: 'FEEDBACK',type: 'link'},
               {state: 'about', name: 'ABOUT',type: 'link'},
               {state: 'contact', name: 'CONTACT',type: 'link'},
               {state: 'search', name: 'SEARCH',type: 'link'},
               {state: 'comingsoon', name: 'COMING SOON',type: 'link'},
               {state: 'maintenance', name: 'MAINTENANCE',type: 'link'},
            ]
         },
         {
            state: 'horizontal/users',
            name: 'USERS',
            type: 'subChild',
            icon: 'web',
            children: [
               {state: 'userlist',type: 'link', name: 'USER LIST'},
               {state: 'userprofile',type: 'link', name: 'USER PROFILE'},
               {state: 'userprofilev2',type: 'link',name: 'USER PROFILE V2'}
            ]
         },
         {
            state: 'horizontal/user-management',
            name: 'MANAGEMENT',
            type: 'subChild',
            icon: 'view_list',
            children: [
               {state: 'usermanagelist', name: 'USER LIST',type: 'link'},
               {state: 'usergridlist', name: 'USER GRID',type: 'link'}
            ]
         },
         {
            state: 'session',
            name: 'SESSIONS',
            type: 'subChild',
            icon: 'face',
            children: [
               {state: 'login', name: 'LOGIN',type: 'link'},
               {state: 'loginV2', name: 'LOGIN V2',type: 'link'},
               {state: 'register', name: 'REGISTER',type: 'link'},
               {state: 'registerV2', name: 'REGISTER V2',type: 'link'},
               {state: 'forgot-password', name: 'FORGOT',type: 'link'},
               {state: 'forgot-passwordV2', name: 'FORGOT V2',type: 'link'},
               {state: 'lockscreen', name: 'LOCKSCREEN',type: 'link'},
               {state: 'lockscreenV2', name: 'LOCKSCREEN V2',type: 'link'}
            ]
         }
      ]
   }
];

@Injectable()
export class HorizontalMenuItems {
  getAll() {
    return MENUITEMS;
  }
}
