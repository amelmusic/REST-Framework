import { Routes } from '@angular/router';

import { UserManageListComponent } from './user-manage-list/user-manage-list.component';
import { UserGridListComponent } from './user-grid-list/user-grid-list.component';

export const UserManagementRoutes: Routes = [
   {
      path: '',
      redirectTo: 'usermanagelist',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'usermanagelist',
            component:  UserManageListComponent
         },
         {
            path: 'usergridlist',
            component:  UserGridListComponent
         }
      ]
   }
];
