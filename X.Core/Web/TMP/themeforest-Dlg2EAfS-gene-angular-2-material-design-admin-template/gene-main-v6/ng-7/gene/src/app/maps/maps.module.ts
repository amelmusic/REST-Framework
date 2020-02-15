import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AgmCoreModule } from '@agm/core';
import { MatCardModule,
			MatMenuModule,
			MatIconModule,
         MatDividerModule,
			MatButtonModule } from '@angular/material';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { FlexLayoutModule } from '@angular/flex-layout';

import { GoogleMapComponent}  from './google-map/googlemap.component';
import { LeafletMapComponent}  from './leaflet-map/leafletmap.component';
import { MapRoutes } from './maps.routing';

@NgModule({
	declarations: [
		GoogleMapComponent,
		LeafletMapComponent
	],
	imports: [
		CommonModule,
		MatCardModule,
		MatButtonModule,
		MatDividerModule,
		MatIconModule,
      MatMenuModule,
      FlexLayoutModule,
      TranslateModule,
		RouterModule.forChild(MapRoutes),
		AgmCoreModule.forRoot({apiKey: 'AIzaSyBtdO5k6CRntAMJCF-H5uZjTCoSGX95cdk'})
	]
})
export class MapsModule { }
