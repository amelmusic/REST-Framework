import { Routes } from '@angular/router';

import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserProfileV2Component } from './user-profile-v2/user-profile-v2.component';
import { UserListComponent } from './user-list/userlist.component';

export const UserRoutes: Routes = [
   {
      path: '',
      redirectTo: 'userlist',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'userlist',
            component: UserListComponent
         },
         {
            path: 'userprofile',
            component:  UserProfileComponent
         },
         {
            path: 'userprofilev2',
            component: UserProfileV2Component
         }
      ]
   }
];
