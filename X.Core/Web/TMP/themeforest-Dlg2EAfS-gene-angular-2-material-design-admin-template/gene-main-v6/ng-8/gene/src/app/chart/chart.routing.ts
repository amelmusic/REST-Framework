import { Routes } from '@angular/router';

import { ChartComponent } from './ng2-charts/chart.component';
import { EasyPieChartComponent } from './easy-pie-chart/easy-pie-chart.component';

export const ChartRoutes: Routes = [
   {
      path: '',
      redirectTo: 'ng2-charts',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'ng2-charts',
            component: ChartComponent
         },
         {
            path: 'easy-pie-chart',
            component: EasyPieChartComponent 
         }
      ]
   }
];
