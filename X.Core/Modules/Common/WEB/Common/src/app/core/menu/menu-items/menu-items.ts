import { Injectable } from '@angular/core';
import { CoreService } from 'app/shared/services/core.service';

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
  permission: string;
  children?: ChildrenItems[];
}

const MENUITEMS = [
  // {
  //   state: '/landing',
  //   name: 'Landing',
  //   type: 'link',
  //   icon: 'explore',
  //   permission: null
  // },
  {
    state: '/users/all',
    name: 'Users',
    type: 'link',
    icon: 'explore',
    permission: null,
    children: [
      {state: 'all', name: 'Views.AspNetUsersList'},
    ]
  },
  {
    state: 'codetables',
    name: 'Codetables',
    type: 'sub',
    icon: 'explore',
    permission: null,
    children: [
      {state: 'template', name: 'Views.TemplateList'},
      {state: 'emails', name: 'Views.EmailList'},
      {state: 'static-data', name: 'Views.StaticDataList'},
    ]
  }
];

@Injectable()
export class MenuItems {
  constructor(private coreService: CoreService) {
	}
  async getAll(): Promise<Menu[]> {
    let items = MENUITEMS;
    let allowedItems = [];
    for(var item of items) {
      var allowed = await this.coreService.permissionCheck({permission: item.permission, operationType: 'View'});
      if (allowed) {
        allowedItems.push(item);
      }
    }

    return allowedItems;
  }
}

