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
    state: 'horizontal',
    name: 'TOP MENU',
    type: 'button',
    icon: 'horizontal_split',
    label: 'New'
  },
  {
    state: 'dashboard',
    name: 'DASHBOARD',
    type: 'sub',
    label:'New',
    icon: 'explore',
    children: [
      {state: 'crm', name: 'CRM', label:'New'},
      {state: 'crypto', name: 'CRYPTO', label:'New'},
      {state: 'courses', name: 'COURSES', label:'New'},
      {state: 'saas', name: 'SAAS'},
      {state: 'web-analytics', name: 'WEB ANALYTICS'},
      
    ]
  },
  {
    state: 'crypto',
    name: 'CRYPTO',
    type: 'sub',
    icon: 'account_balance_wallet',
    label:'New',
    children: [
      {state: 'marketcap', name: 'MARKET CAP'},
      {state: 'wallet', name: 'WALLET'},
      {state: 'trade', name: 'TRADE'}
    ]
  },
  {
    state: 'crm',
    name: 'CRM',
    type: 'sub',
    icon: 'supervised_user_circle',
    label:'New',
    children: [
      {state: 'projects', name: 'PROJECTS'},
      {state: '/project-detail/01', name: 'PROJECT DETAIL'},
      {state: 'clients', name: 'CLIENTS'},
      {state: 'reports', name: 'REPORTS'}
    ]
  },
  {
    state: 'courses',
    name: 'COURSES',
    type: 'sub',
    icon: 'book',
    label:'New',
    children: [
      {state: 'courses-list', name: 'COURSES LIST'},
      {state: 'course-detail', name: 'COURSE DETAIL'},
      {state: 'signin', name: 'SIGN IN'},
      {state: 'payment', name: 'PAYMENT'} 
    ]
  },
  {
    state: 'ecommerce',
    name: 'E-COMMERCE',
    type: 'sub',
    icon: 'explore',
    label:'New',
    children: [
      {state: 'shop', name: 'SHOP WITH ALGOLIA'},
      {state: 'products', name: 'SHOP', label:'New'},
      {state: 'edit-products', name: 'EDIT PRODUCTS', label:'New'},
      {state: 'product-add', name: 'ADD PRODUCT', label:'New'},
      {state: 'cart', name: 'CART'},
      {state: 'checkout', name: 'CHECKOUT'},
      {state: 'cards', name: 'CARDS'},
      {state: 'invoice', name: 'INVOICE'}
    ]
  },
  {
    state: 'pages',
    name: 'PAGES',
    type: 'sub',
    icon: 'import_contacts',
    label : 'New',
    children: [
      {state: 'media', name: 'GALLERY'},
      {state: 'mediaV2', name: 'GALLERY V2',label : 'New'},
      {state: 'pricing', name: 'PRICING'},
      {state: 'pricing-1', name: 'PRICING V2',label : 'New'},
      {state: 'blank', name: 'BLANK'},
      {state: 'timeline', name: 'TIMELINE',label : 'New'},
      {state: 'faq', name: 'FAQ',label : 'New'},
      {state: 'feedback', name: 'FEEDBACK',label : 'New'},
      {state: 'about', name: 'ABOUT',label : 'New'},
      {state: 'contact', name: 'CONTACT',label : 'New'},
      {state: 'search', name: 'SEARCH',label : 'New'},
      {state: 'comingsoon', name: 'COMING SOON',label : 'New'},
      {state: 'maintenance', name: 'MAINTENANCE',label : 'New'},
    ]
  },
  {
    state: 'user-management',
    name: 'MANAGEMENT',
    type: 'sub',
    icon: 'view_list',
    label : 'New',
    children: [
      {state: 'usermanagelist', name: 'USER LIST'},
      {state: 'usergridlist', name: 'USER GRID'}
    ]
  },
  
  {
    state: 'users',
    name: 'USERS',
    type: 'sub',
    icon: 'web',
    label : 'New',
    children: [
      {state: 'userlist', name: 'USER LIST'},
      {state: 'userprofile', name: 'USER PROFILE'},
      {state: 'userprofilev2', name: 'USER PROFILE V2',label: 'New'}
    ]
  },

  {
    state: 'session',
    name: 'SESSIONS',
    type: 'sub',
    icon: 'face',
    label : 'New',
    children: [
      {state: 'login', name: 'LOGIN'},
      {state: 'loginV2', name: 'LOGIN V2',label: 'New'},
      {state: 'register', name: 'REGISTER'},
      {state: 'registerV2', name: 'REGISTER V2',label: 'New'},
      {state: 'forgot-password', name: 'FORGOT'},
      {state: 'forgot-passwordV2', name: 'FORGOT V2',label: 'New'},
      {state: 'lockscreen', name: 'LOCKSCREEN'},
      {state: 'lockscreenV2', name: 'LOCKSCREEN V2',label: 'New'}
    ]
  },
  {
    state: 'video-player',
    name: 'VIDEO PLAYER',
    type: 'link',
    icon: 'videocam',
    label: 'New'
  },
  {
    state: 'taskboard',
    name: 'TASK BOARD',
    type: 'link',
    icon: 'drag_indicator',
    label: 'New'
  },
  {
    state: 'inbox',
    name: 'INBOX',
    type: 'link',
    icon: 'mail'
  },
  {
    state: 'chat',
    name: 'CHAT',
    type: 'link',
    icon: 'chat',
    label: 'New'
  },
  {
    state: 'calendar',
    name: 'CALENDAR',
    type: 'link',
    icon: 'date_range'
  },

  {
    state: 'editor',
    name: 'EDITOR',
    type: 'sub',
    icon: 'format_shapes',
    children: [
      {state: 'wysiwyg', name: 'WYSIWYG EDITOR'},
      {state: 'ckeditor', name: 'CKEDITOR'},
    ]
  },
  {
    state: 'icons',
    name: 'MATERIAL ICONS',
    type: 'link',
    icon: 'grade'
  },
  {
    state: 'chart',
    name: 'CHARTS',
    type: 'sub',
    icon: 'show_chart',
    children: [
      {state: 'ng2-charts', name: 'NG2 CHARTS'},
      {state: 'easy-pie-chart', name: 'EASY PIE'},
    ]
  },
  {
    state: 'components',
    name: 'COMPONENTS',
    type: 'sub',
    icon: 'layers',
    children: [
      {state: 'buttons', name: 'BUTTONS'},
      {state: 'cards', name: 'CARDS'},
      {state: 'grid', name: 'GRID'},
      {state: 'list', name: 'LIST'},
      {state: 'menu', name: 'MENU'},
      {state: 'slider', name: 'SLIDER'},
      {state: 'snackbar', name: 'SNACKBAR'},
      {state: 'tooltip', name: 'TOOLTIP'},
      {state: 'dialog', name: 'DIALOG'},
      {state: 'select', name: 'SELECT'},
      {state: 'input', name: 'INPUT'},
      {state: 'checkbox', name: 'CHECKBOX'},
      {state: 'radio', name: 'RADIO'},
      {state: 'toolbar', name: 'TOOLBAR'},
      {state: 'progress', name: 'PROGRESS'},
      {state: 'tabs', name: 'TABS'},
      {state: 'colorpicker', name: 'COLORPICKER'},
      {state: 'datepicker', name: 'DATEPICKER'},
    ]
  },
  {
    state: 'dragndrop',
    name: 'DRAG & DROP',
    type: 'sub',
    icon: 'mouse',
    children: [
      {state: 'dragula', name: 'DRAGULA'},
      {state: 'sortable', name: 'SORTABLEJS'}
    ]
  },
  {
    state: 'tables',
    name: 'TABLES',
    type: 'sub',
    icon: 'format_line_spacing',
    children: [
      {state: 'fullscreen', name: 'FULLSCREEN'},
      {state: 'selection', name: 'SELECTION'},
      {state: 'pinning', name: 'PINNING'},
      {state: 'sorting', name: 'SORTING'},
      {state: 'paging', name: 'PAGING'},
      {state: 'editing', name: 'EDITING'},
      {state: 'filter', name: 'FILTER'},
      {state: 'responsive', name: 'RESPONSIVE'}
    ]
  },
  {
    state: 'forms',
    name: 'FORMS',
    type: 'sub',
    icon: 'insert_comment',
    children: [
      {state: 'form-wizard', name: 'FORM WIZARD'},
      {state: 'form-validation', name: 'FORM VALIDATION'},
      {state: 'form-upload', name: 'UPLOAD'},
      {state: 'form-tree', name: 'TREE'}
    ]
  },
  {
    state: 'maps',
    name: 'MAPS',
    type: 'sub',
    icon: 'map',
    children: [
      {state: 'googlemap', name: 'GOOGLE MAP'},
      {state: 'leafletmap', name: 'LEAFLET MAP'}
    ]
  },


];

@Injectable()
export class MenuItems {
  getAll(): Menu[] {
    return MENUITEMS;
  }
  add(menu:any) {
    MENUITEMS.push(menu);
  }
}
