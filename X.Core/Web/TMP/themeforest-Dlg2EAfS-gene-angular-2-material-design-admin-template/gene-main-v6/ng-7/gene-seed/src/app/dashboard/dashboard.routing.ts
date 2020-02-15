import { Routes } from '@angular/router';

import { DashboardComponent } from './dashboard-v1/dashboard.component';

export const DashboardRoutes: Routes = [
   {
      path: '',
      redirectTo: 'dashboard-v1',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'dashboard-v1',
            component: DashboardComponent
         }
      ]
   }
];
