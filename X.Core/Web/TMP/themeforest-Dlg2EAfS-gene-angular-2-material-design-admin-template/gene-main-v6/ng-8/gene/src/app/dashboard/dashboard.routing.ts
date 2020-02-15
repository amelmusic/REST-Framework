import { Routes } from '@angular/router';

import { CoursesComponent } from './courses/courses.component';
import { SaasComponent } from './saas/saas.component';
import { WebAnalyticsComponent } from './web-analytics/webanalytics.component';
import { CryptoComponent } from './crypto/crypto.component';
import { CrmComponent } from './crm/crm.component';

export const DashboardRoutes: Routes = [
   {
      path: '',
      redirectTo: 'crm',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'saas',
            component: SaasComponent
         },
         {
            path: 'web-analytics',
            component: WebAnalyticsComponent 
         },
         {
            path: "crypto",
            component : CryptoComponent
         },
         {
            path: "crm",
            component : CrmComponent
         },
         {
            path: "courses",
            component : CoursesComponent
         }
      ]
   }
];
