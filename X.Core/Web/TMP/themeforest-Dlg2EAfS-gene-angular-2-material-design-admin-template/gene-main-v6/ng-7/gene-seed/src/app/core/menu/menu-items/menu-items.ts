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
    state: 'dashboard',
    name: 'DASHBOARD',
    type: 'sub',
    icon: 'explore',
    children: [
      {state: 'dashboard-v1', name: 'DASHBOARD 1'}
    ]
  },
  {
    state: 'session',
    name: 'SESSIONS',
    type: 'sub',
    icon: 'face',
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
  }
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
