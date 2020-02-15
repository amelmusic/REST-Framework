import { Routes } from '@angular/router';

import { GoogleMapComponent}  from './google-map/googlemap.component';
import { LeafletMapComponent}  from './leaflet-map/leafletmap.component';

export const MapRoutes: Routes = [
   {
      path: '',
      redirectTo: 'googlemap',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'googlemap',
            component: GoogleMapComponent
         },
         {
            path: 'leafletmap',
            component:  LeafletMapComponent
         }
      ]
   }
];
