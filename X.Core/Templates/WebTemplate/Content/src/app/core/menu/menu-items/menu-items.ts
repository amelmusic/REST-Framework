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
    state: 'xcore',
    name: 'XCore',
    type: 'sub',
    icon: 'explore',
    children: [
      //#if (!excludeDummyModule)
      {state: 'country', name: 'Views.CountryList'},
      //#endif 
    ]
  }
];

@Injectable()
export class MenuItems {
  getAll(): Menu[] {
    return MENUITEMS;
  }
}
