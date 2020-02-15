import { Routes } from '@angular/router';

import { FullscreenTableComponent}  from './table-fullscreen/table-fullscreen.component';
import { EditingTableComponent}  from './table-editing/table-editing.component';
import { FilterTableComponent}  from './table-filter/table-filter.component';
import { PagingTableComponent}  from './table-paging/table-paging.component';
import { SortingTableComponent}  from './table-sorting/table-sorting.component';
import { PinningTableComponent}  from './table-pinning/table-pinning.component';
import { SelectionTableComponent}  from './table-selection/table-selection.component';
import { ResponsiveTableComponent}  from './table-responsive/table-responsive.component';


export const TablesRoutes: Routes = [
   {
      path: '',
      redirectTo: 'fullscreen',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'fullscreen',
            component: FullscreenTableComponent
         },
         {
            path: 'editing',
            component: EditingTableComponent
         },
         {
            path: 'filter',
            component: FilterTableComponent
         },
         {
            path: 'paging',
            component: PagingTableComponent
         },
         {
            path: 'sorting',
            component: SortingTableComponent
         },
         {
            path: 'pinning',
            component: PinningTableComponent
         },
         {
            path: 'selection',
            component: SelectionTableComponent
         },
         {
            path: 'responsive',
            component: ResponsiveTableComponent
         }
      ]
   }
];
