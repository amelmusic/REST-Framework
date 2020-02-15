import { Routes } from '@angular/router';

import { DragulaDemoComponent } from './dragula/dragula.component';
import { SortableDemoComponent } from './sortablejs/sortable.component';

export const DragDropRoutes: Routes = [
   {
      path: '',
      redirectTo: 'dragula',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'dragula',
            component: DragulaDemoComponent
         },
         {
            path: 'sortable',
            component: SortableDemoComponent 
         }
      ]
   }
];
