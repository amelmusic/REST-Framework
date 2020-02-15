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
  {
    state: 'xcore',
    name: 'XCore',
    type: 'sub',
    icon: 'explore',
    permission: 'xcore',
    children: [
      //#if (!excludeDummyModule)
      {state: 'country', name: 'Views.CountryList'},
      //#endif 
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

